<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
		CodeBehind="voucher-example.aspx.cs" Inherits="web.VoucherExample" %>
<%@ Register src="controls/Rendering.ascx" tagname="Rendering" tagprefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="Left" runat="server" ContentPlaceHolderID="LeftContent">
	<fieldset>
		<legend>Poster Variables</legend>			
		<ul class="noBullets">
			<li>
				<label for="<%=SpecialOffer.ClientID%>">Special offer?</label>
				<asp:TextBox ID="SpecialOffer" runat="server" CssClass="SpecialOffer" TextMode="MultiLine" Text="In celebration, have a pitch at half price this week on us!" />
			</li>
			<li>
				<label for="<%=OfferFor.ClientID%>">Name of the person the offer is for?</label>
				<asp:TextBox ID="OfferFor" runat="server" Text="Fred" />
			</li>
			<li>
				<label for="<%=Birthday.ClientID%>">Birthday (dd-mm-yyyy)?</label>
				<asp:TextBox ID="Birthday" runat="server" Text="12-11-1955" />
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

