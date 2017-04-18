using System;

namespace SvgNet.SvgTypes
{
	public class SvgXRef : ICloneable
	{
		private string _href;

		private string _type = "simple";

		private string _role;

		private string _arcrole;

		private string _title;

		private string _show;

		private string _actuate = "onLoad";

		public string Href
		{
			get
			{
				return this._href;
			}
			set
			{
				this._href = value;
			}
		}

		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public string Role
		{
			get
			{
				return this._role;
			}
			set
			{
				this._role = value;
			}
		}

		public string Arcrole
		{
			get
			{
				return this._arcrole;
			}
			set
			{
				this._arcrole = value;
			}
		}

		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		public string Show
		{
			get
			{
				return this._show;
			}
			set
			{
				this._show = value;
			}
		}

		public string Actuate
		{
			get
			{
				return this._actuate;
			}
			set
			{
				this._actuate = value;
			}
		}

		public SvgXRef()
		{
		}

		public SvgXRef(string href)
		{
			this._href = href;
		}

		public SvgXRef(SvgStyledTransformedElement el)
		{
			this.ReadFromElement(el);
		}

		public object Clone()
		{
			return new SvgXRef
			{
				Href = this.Href,
				Type = this.Type,
				Role = this.Role,
				Arcrole = this.Arcrole,
				Title = this.Title,
				Show = this.Show,
				Actuate = this.Actuate
			};
		}

		public override string ToString()
		{
			return this._href;
		}

		public void WriteToElement(SvgStyledTransformedElement el)
		{
			el["xlink:href"] = this._href;
			el["xlink:role"] = this._role;
			el["xlink:arcrole"] = this._arcrole;
			el["xlink:title"] = this._title;
			el["xlink:show"] = this._show;
		}

		public void ReadFromElement(SvgStyledTransformedElement el)
		{
			this._href = (string)el["xlink:href"];
			this._role = (string)el["xlink:role"];
			this._arcrole = (string)el["xlink:arcrole"];
			this._title = (string)el["xlink:title"];
			this._show = (string)el["xlink:show"];
		}
	}
}
