<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Rendering.ascx.cs" Inherits="web.controls.Rendering" %>

<fieldset>
	<legend>Poster Rendering</legend>			
	<ul class="noBullets">
		<li>
			<label for="<%=Size.ClientID%>">Size of poster?</label>
			<asp:DropDownList ID="ddlSize" runat="server">
				<asp:ListItem Text="Double Size" Value="200" />
				<asp:ListItem Text="Full Size" Value="100"/>
				<asp:ListItem Text="75%" Value="75" />
				<asp:ListItem Text="50%" Value="50" />
				<asp:ListItem Text="25%" Value="25" />
				<asp:ListItem Text="20%" Value="20" Selected="true" />
				<asp:ListItem Text="10%" Value="10" />
			</asp:DropDownList>
		</li>
		<li>
			<label for="<%=Size.ClientID%>">Format of poster?</label>
			<asp:DropDownList ID="ddlImageTypes" runat="server" />
		</li>
		<li>
			<label for="<%=ShowGuides.ClientID%>">Show Guides on poster?</label>
			<asp:CheckBox ID="chkShowGuides" runat="server" Checked="false" />
		</li>
		<li>
			<label for="<%=txtApiKey.ClientID %>">Google [static] Maps API Key</label>
			<asp:TextBox ID="txtApiKey" runat="server" />
		</li>
	</ul>
</fieldset>



