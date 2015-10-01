using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WeatherApplication2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void SignOutButton_Click(object sender, EventArgs e)
        {
            SignInTextBox.Text = "";
            CreateNewTextBox.Text = "";
            SavedLocations.Items.Clear();
            SignedInLabel.Text = "";
        }

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            var list = new List<string>();
            SqlConnection cn = new SqlConnection
                    (ConfigurationManager.ConnectionStrings["WeatherAppDBASEConnectionString"]
                    .ConnectionString);
            cn.Open();
            string checkQuery = "SELECT COUNT(*) FROM [Table] WHERE ([username] = @username)";
            SqlCommand cmd = new SqlCommand(checkQuery, cn);
            cmd.Parameters.AddWithValue("@username", SignInTextBox.Text);
            int temp = (int)cmd.ExecuteScalar();

            if (temp > 0)
            {
                //Name already exists
                ResultLabel.Text = "THE NAME EXISTS";
                SavedLocations.Items.Clear();

                //loadsavedlocations//
                string getLocationsQuery = "SELECT [location1],[location2],[location3] FROM [Table] WHERE ([username] = @username)";

                using (var command = new SqlCommand(getLocationsQuery, cn))
                {
                    command.Parameters.AddWithValue("@username", SignInTextBox.Text.ToLower());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                            list.Add(reader.GetString(1));
                            list.Add(reader.GetString(2));
                        }
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    SavedLocations.Items.Add(list[i]);
                }

                SignedInLabel.Text = SignInTextBox.Text.ToLower();
            }
            else
            {
                //Name does not exist
                ResultLabel.Text = "THE NAME DOES NOT EXIST";
            }
            
            cn.Close();
        }

        protected void CreateNewButton_Click(object sender, EventArgs e)
        {
            var list = new List<string>();
            SqlConnection cn = new SqlConnection
                    (ConfigurationManager.ConnectionStrings["WeatherAppDBASEConnectionString"]
                    .ConnectionString);
            cn.Open();
            string checkQuery = "SELECT COUNT(*) FROM [Table] WHERE ([username] = @username)";
            SqlCommand cmd = new SqlCommand(checkQuery, cn);
            cmd.Parameters.AddWithValue("@username", CreateNewTextBox.Text);
            int temp = (int)cmd.ExecuteScalar();

            if (temp > 0)
            {
                //Name already exists
                ResultLabel.Text = "THE NAME EXISTS";
            }
            else
            {
                //Name does not exist
                ResultLabel.Text = "THE NAME DOES NOT EXIST";
                SavedLocations.Items.Clear();

                //insert new name into database//
                string insertQuery = "INSERT INTO [Table] ([username],[location1],[location2],[location3]) VALUES (@user, @val1, @val2, @val3)";
                using (SqlCommand comm = new SqlCommand(insertQuery, cn))
                {
                    comm.Parameters.AddWithValue("@user", CreateNewTextBox.Text.ToLower());
                    comm.Parameters.AddWithValue("@val1", " ");
                    comm.Parameters.AddWithValue("@val2", " ");
                    comm.Parameters.AddWithValue("@val3", " ");
                    comm.ExecuteNonQuery();
                }

                //loadsavedlocations//
                string getLocationsQuery = "SELECT [location1],[location2],[location3] FROM [Table] WHERE ([username] = @username)";

                using (var command = new SqlCommand(getLocationsQuery, cn))
                {
                    command.Parameters.AddWithValue("@username", CreateNewTextBox.Text.ToLower());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                            list.Add(reader.GetString(1));
                            list.Add(reader.GetString(2));
                        }
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    SavedLocations.Items.Add(list[i]);
                }

                SignedInLabel.Text = CreateNewTextBox.Text.ToLower();
            }

            cn.Close();
        }

        protected void SaveLocationBtn_Click(object sender, EventArgs e)
        {
            if (SignedInLabel.Text != "")
            {
                string locationToSave = CityTextBox.Text.Replace(' ', '_') + " " + StateDropBox.SelectedItem.Value.ToString();
                var list = new List<string>();

                //open connection
                SqlConnection cn = new SqlConnection
                    (ConfigurationManager.ConnectionStrings["WeatherAppDBASEConnectionString"]
                    .ConnectionString);
                cn.Open();

                //loadcurrentlocations//
                string getCurrentQuery = "SELECT [location1],[location2],[location3] FROM [Table] WHERE ([username] = @username)";

                using (var command = new SqlCommand(getCurrentQuery, cn))
                {
                    command.Parameters.AddWithValue("@username", SignedInLabel.Text);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                            list.Add(reader.GetString(1));
                            list.Add(reader.GetString(2));
                        }
                    }
                }

                //save location to database
                string updateQuery = "UPDATE [Table] SET [location1]=@val1, [location2]=@val2, [location3]=@val3 WHERE [username]=@username";
                using (SqlCommand cmd = new SqlCommand(updateQuery, cn))
                {
                    cmd.Parameters.AddWithValue("@username", SignedInLabel.Text);
                    cmd.Parameters.AddWithValue("@val3", list[1]);
                    cmd.Parameters.AddWithValue("@val2", list[0]);
                    cmd.Parameters.AddWithValue("@val1", locationToSave);
                    cmd.ExecuteNonQuery();
                }

                //loadsavedlocations//
                SavedLocations.Items.Clear();
                list.Clear();
                string getLocationsQuery = "SELECT [location1],[location2],[location3] FROM [Table] WHERE ([username] = @username)";

                using (var command = new SqlCommand(getLocationsQuery, cn))
                {
                    command.Parameters.AddWithValue("@username", SignedInLabel.Text);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                            list.Add(reader.GetString(1));
                            list.Add(reader.GetString(2));
                        }
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    SavedLocations.Items.Add(list[i]);
                }


                //refresh dropdown list//
                SavedLocations.Items.Add(locationToSave);

                cn.Close();
            }
        }

        protected void GetWeatherBtn_Click(object sender, EventArgs e)
        {
            //Start Pulling Weather Code
            string wundergroundAPI_key = "244dbe78d6953de3";
            string state = StateDropBox.SelectedItem.Value.ToString();
            string city = CityTextBox.Text.Replace(' ', '_');

            //Calls the dissection method with the proper constructed request
            parse("http://api.wunderground.com/api/" + wundergroundAPI_key + "/conditions/q/" + state + "/" + city + ".xml");
        }

        protected void GetWeatherBtn2_Click(object sender, EventArgs e)
        {
            //Start Pulling Weather Code
            string wundergroundAPI_key = "244dbe78d6953de3";
            string[] locationArray = SavedLocations.SelectedItem.Value.ToString().Split(' ');
            string city = locationArray[0];
            string state = locationArray[1];

            //Calls the dissection method with the proper constructed request
            parse("http://api.wunderground.com/api/" + wundergroundAPI_key + "/conditions/q/" + state + "/" + city + ".xml");
        }

        //Takes urlxml request, downloads it, parses it, and displays the parsed data
        private void parse(string urlxml)
        {
            //Variables that will be parsed from the api
            string place = "";
            string observation_time = "";
            string weather1 = "";
            string temperature_string = "";
            string relative_humidity = "";
            string wind_string = "";
            string pressure_mb = "";
            string dewpoint_string = "";
            string visibility_km = "";
            string latitude = "";
            string longitude = "";

            //Declares client and downloads the urlxml for parsing
            var client = new WebClient();
            string urlRequest = client.DownloadString(urlxml);

            //An xmlreader is assigned to the inputted urlxml
            using (XmlReader reader = XmlReader.Create(new StringReader(urlRequest)))
            {
                //Read data from the reader
                while (reader.Read())
                {
                    //For all the nodes in the reader, if the NodeType is Element; perform the following cases to parse the data
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.Equals("full"))
                            {
                                reader.Read();
                                place = reader.Value;
                            }
                            else if (reader.Name.Equals("observation_time"))
                            {
                                reader.Read();
                                observation_time = reader.Value;
                            }
                            else if (reader.Name.Equals("weather"))
                            {
                                reader.Read();
                                weather1 = reader.Value;
                            }
                            else if (reader.Name.Equals("temperature_string"))
                            {
                                reader.Read();
                                temperature_string = reader.Value;
                            }
                            else if (reader.Name.Equals("relative_humidity"))
                            {
                                reader.Read();
                                relative_humidity = reader.Value;
                            }
                            else if (reader.Name.Equals("wind_string"))
                            {
                                reader.Read();
                                wind_string = reader.Value;
                            }
                            else if (reader.Name.Equals("pressure_mb"))
                            {
                                reader.Read();
                                pressure_mb = reader.Value;
                            }
                            else if (reader.Name.Equals("dewpoint_string"))
                            {
                                reader.Read();
                                dewpoint_string = reader.Value;
                            }
                            else if (reader.Name.Equals("visibility_km"))
                            {
                                reader.Read();
                                visibility_km = reader.Value;
                            }
                            else if (reader.Name.Equals("latitude"))
                            {
                                reader.Read();
                                latitude = reader.Value;
                            }
                            else if (reader.Name.Equals("longitude"))
                            {
                                reader.Read();
                                longitude = reader.Value;
                            }

                            break;
                    }
                }
            }

            //Display each of the variables that were obtained through the parsing
            ResultsLbl1.Text = place;
            ResultsLbl2.Text = observation_time;
            ResultsLbl3.Text = weather1;
            ResultsLbl4.Text = temperature_string;
            ResultsLbl5.Text = relative_humidity;
            ResultsLbl6.Text = wind_string;
            ResultsLbl7.Text = pressure_mb;
            ResultsLbl8.Text = dewpoint_string;
            ResultsLbl9.Text = visibility_km;
            ResultsLbl10.Text = longitude + ", " + latitude;
        }
    }
}