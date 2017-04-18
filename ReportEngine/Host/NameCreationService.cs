using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace Host
{
    /// <summary>
    /// This is responsible for naming the components as they are created.
    /// This is added as a servide by the HostSurfaceManager
    /// </summary>
	public class NameCreationServiceImpl : INameCreationService
	{
        public NameCreationServiceImpl() { }

        public string CreateName ( IContainer container, Type type ) {
            if ( null == container )
                return string.Empty;

            string itemTypeDisplayName = type.Name;
            DisplayNameAttribute dna = TypeDescriptor.GetAttributes(type)[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
            if (dna != null)
            {
                if (!string.IsNullOrEmpty(dna.DisplayName))
                    itemTypeDisplayName = dna.DisplayName;
            }

            ComponentCollection cc = container.Components;
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            int count = 0;

            int i = 0;
            while ( i < cc.Count ) {
                IComponent comp = cc[i] as IComponent;

                if ( comp.GetType() == type )	{

                    string name = comp.Site.Name;
                    if (name.StartsWith(itemTypeDisplayName,true,System.Globalization.CultureInfo.CurrentCulture))
                    {
                        try	{
                            int value = Int32.Parse(name.Substring(itemTypeDisplayName.Length));
                            if ( value < min ) min = value;
                            if ( value > max ) max = value;
                            count++;
                        }
                        catch ( Exception ) {}
                    }//end_if
                }//end_if
                i++;
            } //end_while

            if ( 0 == count ) {
                return itemTypeDisplayName + "1";
            }
            else if ( min > 1 ) {
                int j = min - 1;
                return itemTypeDisplayName + j.ToString();
            }
            else {
                int j = max + 1;
                return itemTypeDisplayName + j.ToString();
            }
        }

        public bool IsValidName ( string name ) {
            //- Check that name is "something" and that is a string with at least one char
            if ( String.IsNullOrEmpty ( name ) )
                return false;

            //- then the first character must be a letter
            if ( ! ( char.IsLetter ( name, 0 ) ) )
                return false;

            //- then don't allow a leading underscore
            if ( name.StartsWith ( "_" ) )
                return false;

            //- ok, it's a valid name
            return true;
        }

        public void ValidateName ( string name ) {
            //-  Use our existing method to check, if it's invalid throw an exception
            if ( ! ( IsValidName ( name ) ) )
                throw new ArgumentException ( "Invalid name: " + name );
        }

	}// class
}// namespace
