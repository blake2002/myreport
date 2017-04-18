using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public class KeywordInfo
    {
        // Fields
        public string AppliesToProperties = string.Empty;
        public string AppliesToTypes = string.Empty;
        public string Description = string.Empty;
        public string Keyword = string.Empty;
        public string KeywordAliases = string.Empty;
        public string Name = string.Empty;
        public bool SupportsFormatting;
        public bool SupportsValueIndex;

        // Methods
        public KeywordInfo(string name, string keyword, string keywordAliases, string description, string appliesToTypes, string appliesToProperties, bool supportsFormatting, bool supportsValueIndex)
        {
            this.Name = name;
            this.Keyword = keyword;
            this.KeywordAliases = keywordAliases;
            this.Description = description;
            this.AppliesToTypes = appliesToTypes;
            this.AppliesToProperties = appliesToProperties;
            this.SupportsFormatting = supportsFormatting;
            this.SupportsValueIndex = supportsValueIndex;
        }

        public string[] GetKeywords()
        {
            if (this.KeywordAliases.Length > 0)
            {
                string[] strArray = this.KeywordAliases.Split(new char[] { ',' });
                string[] array = new string[strArray.Length + 1];
                array[0] = this.Keyword;
                strArray.CopyTo(array, 1);
                return array;
            }
            return new string[] { this.Keyword };
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
