<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeatherApplication2._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WeatherAppDBASEConnectionString %>" SelectCommand="SELECT * FROM [Table]"></asp:SqlDataSource>
            <hgroup class="title">
                <h2>Enter a City and State:</h2>
            </hgroup>
            <p>
                <asp:TextBox ID="CityTextBox" runat="server" ToolTip="Type a city"></asp:TextBox>
                <asp:DropDownList ID="StateDropBox" runat="server">
                    <asp:ListItem>AL</asp:ListItem>
                    <asp:ListItem>AK</asp:ListItem>
                    <asp:ListItem>AZ</asp:ListItem>
                    <asp:ListItem>AR</asp:ListItem>
                    <asp:ListItem>CA</asp:ListItem>
                    <asp:ListItem>CO</asp:ListItem>
                    <asp:ListItem>CT</asp:ListItem>
                    <asp:ListItem>DE</asp:ListItem>
                    <asp:ListItem>FL</asp:ListItem>
                    <asp:ListItem>GA</asp:ListItem>
                    <asp:ListItem>HI</asp:ListItem>
                    <asp:ListItem>ID</asp:ListItem>
                    <asp:ListItem>IL</asp:ListItem>
                    <asp:ListItem>IN</asp:ListItem>
                    <asp:ListItem>IA</asp:ListItem>
                    <asp:ListItem>KS</asp:ListItem>
                    <asp:ListItem>KY</asp:ListItem>
                    <asp:ListItem>LA</asp:ListItem>
                    <asp:ListItem>ME</asp:ListItem>
                    <asp:ListItem>MD</asp:ListItem>
                    <asp:ListItem>MA</asp:ListItem>
                    <asp:ListItem>MI</asp:ListItem>
                    <asp:ListItem>MN</asp:ListItem>
                    <asp:ListItem>MS</asp:ListItem>
                    <asp:ListItem>MO</asp:ListItem>
                    <asp:ListItem>MT</asp:ListItem>
                    <asp:ListItem>NE</asp:ListItem>
                    <asp:ListItem>NV</asp:ListItem>
                    <asp:ListItem>NH</asp:ListItem>
                    <asp:ListItem>NJ</asp:ListItem>
                    <asp:ListItem>NM</asp:ListItem>
                    <asp:ListItem>NY</asp:ListItem>
                    <asp:ListItem>NC</asp:ListItem>
                    <asp:ListItem>ND</asp:ListItem>
                    <asp:ListItem>OH</asp:ListItem>
                    <asp:ListItem>OK</asp:ListItem>
                    <asp:ListItem>OR</asp:ListItem>
                    <asp:ListItem>PA</asp:ListItem>
                    <asp:ListItem>RI</asp:ListItem>
                    <asp:ListItem>SC</asp:ListItem>
                    <asp:ListItem>SD</asp:ListItem>
                    <asp:ListItem>TN</asp:ListItem>
                    <asp:ListItem>TX</asp:ListItem>
                    <asp:ListItem>UT</asp:ListItem>
                    <asp:ListItem>VT</asp:ListItem>
                    <asp:ListItem>VA</asp:ListItem>
                    <asp:ListItem>WA</asp:ListItem>
                    <asp:ListItem>WV</asp:ListItem>
                    <asp:ListItem>WI</asp:ListItem>
                    <asp:ListItem>WY</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <asp:Button ID="GetWeatherBtn" runat="server" Text="Get Weather" Width="123px" OnClick="GetWeatherBtn_Click" />
                <asp:Button ID="SaveLocationBtn" runat="server" Text="Save Location" Width="140px" OnClick="SaveLocationBtn_Click" />
            </p>
            <hgroup class="title">
                <h2>Or Select One That You Have Saved:</h2>
            </hgroup>
            <p>
                <asp:TextBox ID="SignInTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="SignInButton" runat="server" Text="Sign in" Height="32px" OnClick="SignInButton_Click" />
            </p>
            <p>
                <asp:TextBox ID="CreateNewTextBox" runat="server"></asp:TextBox>
                <asp:Button ID="CreateNewButton" runat="server" Text="Create New" Height="32px" OnClick="CreateNewButton_Click" />
            </p>
            <p>
                <asp:DropDownList ID="SavedLocations" runat="server"></asp:DropDownList>
                <asp:Button ID="GetWeatherBtn2" runat="server" Text="Get Weather" Width="123px" OnClick="GetWeatherBtn2_Click" Height="32px" />
            </p>
            <p>
                <asp:Button ID="SignOutButton" runat="server" Text="Sign Out" Height="32px" OnClick="SignOutButton_Click" />
            </p>
            <p>
                <asp:Label ID="ResultLabel" runat="server" Text="N/A"></asp:Label>
            </p>
            <p>
                <asp:Label ID="SignedInLabel" runat="server" Text="" BorderColor="Black" BorderStyle="Groove"></asp:Label>
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label ID="Label1" runat="server" Text="Weather For"></asp:Label>
    <ol class="round">
        <li>
            <h5>Place</h5>
            <asp:Label ID="ResultsLbl1" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Observation Time</h5>
            <asp:Label ID="ResultsLbl2" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Weather</h5>
            <asp:Label ID="ResultsLbl3" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Temperature</h5>
            <asp:Label ID="ResultsLbl4" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Relative Humidity</h5>
            <asp:Label ID="ResultsLbl5" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Wind</h5>
            <asp:Label ID="ResultsLbl6" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Pressure (mb)</h5>
            <asp:Label ID="ResultsLbl7" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Dewpoint</h5>
            <asp:Label ID="ResultsLbl8" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Visibility (km)</h5>
            <asp:Label ID="ResultsLbl9" runat="server" Text="N/A"></asp:Label>
        </li>
        <li>
            <h5>Location</h5>
            <asp:Label ID="ResultsLbl10" runat="server" Text="N/A"></asp:Label>
        </li>
    </ol>
</asp:Content>
