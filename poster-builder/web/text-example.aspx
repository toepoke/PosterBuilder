<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
		CodeBehind="text-example.aspx.cs" Inherits="web.TextExample" %>
<%@ Register src="controls/Rendering.ascx" tagname="Rendering" tagprefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="Left" runat="server" ContentPlaceHolderID="LeftContent">
	<fieldset>
		<legend>Drawing Preferences</legend>			
		<ul class="noBullets">
			<li>
				<label for="<%=Frequency.ClientID%>">How often do you play?</label>
				<asp:TextBox ID="Frequency" runat="server" Text="Every thursday at 6pm" />
			</li>
			<li>
				<label for="<%=Venue.ClientID%>">Where do you play?</label>
				<asp:TextBox ID="Venue" runat="server" Text="The local park, with coats for posts" />
			</li>
			<li>
				<label for="<%=EventID.ClientID%>">What's the ID of your game?</label>
				<asp:TextBox ID="EventID" runat="server" Text="3876" />
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
	<asp:Image ID="Poster" runat="server" Visible="false" />	
</asp:Content>

