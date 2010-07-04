using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Provider
{
    public class Provider
    {
        private Provider()
		{ }

		private static IProvider _instance = null;
		private static object lockHelper = new object();

		static Provider()
		{
			GetProvider();
		}

		private static void GetProvider()
		{
			try
			{
                _instance = (IProvider)Activator.CreateInstance(Type.GetType(string.Format("NConvert.{0}.Provider, NConvert.{0}", MainForm.cic.ConvertTypeName), false, true));
			}
			catch(Exception exProvider)
			{
				throw exProvider;
			}
		}

		public static IProvider GetInstance()
		{
			if (_instance == null)
			{
				lock (lockHelper)
				{
					if (_instance == null)
					{
						GetProvider();
					}
				}
			}
			return _instance;
		}

        public static void ResetProvider()
        {
            _instance = null; 
        }
    }
}
