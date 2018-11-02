using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.controls
{
	public partial class Rendering : System.Web.UI.UserControl
	{
		public DropDownList Size { 
			get { return this.ddlSize; }
		}

		public DropDownList ImageTypes {
			get { return this.ddlImageTypes; } 
		}

		public CheckBox ShowGuides {
			get { return this.chkShowGuides; }
		}

		public TextBox ApiKey {
			get { return this.txtApiKey; }
		}

		public string ToQueryString() {
			return string.Format("show-guides={0}&type={1}&size={2}", 
				this.ShowGuides.Checked.ToString(), 
				this.ImageTypes.SelectedValue.ToString(),
				this.Size.SelectedValue.ToString()
			);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack) {
				LoadImageTypes();
				this.ApiKey.Text = ConfigurationManager.AppSettings["GoogleStaticMapsApiKey"] as string;
			}
		}

		private void LoadImageTypes() {
			
			ImageTypes.DataSource = Enum.GetNames(typeof(PosterBuilder.ImgFormat.SupportedTypes));
			ImageTypes.DataBind();

			// JPEG by default
			ImageTypes.SelectedValue = PosterBuilder.ImgFormat.SupportedTypes.Png.ToString();
		}

	}
}