using System;
using System.Collections.Generic;
using System.Text;


namespace jsCompilerUtility
{

    public class CommandLine
    {

        private System.Collections.ArrayList m_strArgs = new System.Collections.ArrayList();

        public CommandLine()
        {
        }
        public CommandLine(string[] args)
        {
            Initialize(args);
        }

        public bool Initialize(string[] args)
        {
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    m_strArgs.Add(args[i]);
                }
            }
            return true;
        }
        public int Count
        {
            get { return m_strArgs.Count; }
        }
        public string this[int i]
        {
            get { return (string)m_strArgs[i]; }
        }
        //		public string Item(int i){
        //			return (string)m_strArgs[i];
        //		}
        public int GetIndex(string strName)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ToLower() == strName.ToLower()) return i;
            }
            return -1;
        }
        public bool Exist(string strName)
        {
            return GetIndex(strName) != -1;
        }
        public string Arguments(int index)
        {
            return (string)m_strArgs[index];
        }
        public string Arguments(string strName)
        {
            return Arguments(strName, null);
        }
        public bool Arguments(string strName, bool booDefaultValue)
        {
            string s = this.Arguments(strName, booDefaultValue.ToString());
            return s.ToLower() == "true";
        }
        public int Arguments(string strName, int intDefaultValue)
        {
            string s = this.Arguments(strName, intDefaultValue.ToString());
            if(!String.IsNullOrEmpty(s))
                return int.Parse(s);
            return intDefaultValue;
        }
        public string Arguments(string strName, string strDefaultValue)
        {
            int lngIndex = GetIndex(strName);
            if (lngIndex != -1)
            {
                return this[lngIndex + 1];
            }
            else
            {
                return strDefaultValue;
            }
        }
    }


}
