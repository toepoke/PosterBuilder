<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
		CodeBehind="map-example.aspx.cs" Inherits="web.MapExample" %>
<%@ Register src="controls/Rendering.ascx" tagname="Rendering" tagprefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="Left" runat="server" ContentPlaceHolderID="LeftContent">
	<fieldset>
		<legend>Poster Variables</legend>			
		<ul class="noBullets">
			<li>
				<label for="<%=Frequency.ClientID%>">How often do you play?</label>
				<asp:TextBox ID="Frequency" runat="server" Text="Every thursday at 6pm" />
			</li>
			<li>
				<label for="<%=Venue.ClientID%>">Where do you play?</label>
				<asp:TextBox ID="Venue" runat="server" Text="local park, with coats for posts" />
			</li>
			<li>
				<label for="<%=EventID.ClientID%>">What's the ID of your game?</label>
				<asp:TextBox ID="EventID" runat="server" Text="3876" />
			</li>
			<li>
				<label for="LatLong">Map Latitude/Longitude (lat, long)</label>
				<asp:TextBox ID="LatLong" runat="server" Text="53.77800, -1.57194" />
			</li>
			<li>
				<label for="Address">Map Address</label>
				<asp:TextBox ID="Address" runat="server" Text="Wembley stadium, london" />
			</li>
			<li>
				<label for="MapType">Map type</label>
				<asp:DropDownList ID="MapType" runat="server">
					<asp:ListItem Selected="True" Text="Hybrid" Value="Hybrid" />
					<asp:ListItem Text="Road" Value="Road" />
					<asp:ListItem Text="Satellite" Value="Satellite" />
				</asp:DropDownList>
			</li>
		</ul>
	</fieldset>

	<hr />

	<uc1:Rendering ID="PosterRendering" runat="server" />

	<div class="buttons">
		<asp:Button ID="Preview" runat="server" Text="Preview" />
		<asp:Button ID="Download" runat="server" Text="Download" />
	</div>

</asp:Content>

<asp:Content ID="Right" runat="server" ContentPlaceHolderID="RightContent">
	<asp:Image ID="Poster" CssClass="poster" runat="server" Visible="false" />	
</asp:Content>

