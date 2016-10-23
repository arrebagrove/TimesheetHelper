using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetHelper.ModelView
{
    public class SettingsViewModel: INotifyPropertyChanged
    {
        private Settings settingsRef = Settings.CurrentSettings;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            settingsRef = Settings.CurrentSettings;
        }

        public bool AutoSave
        {
            get
            {
                return settingsRef.AutoWrite;
            }

            set
            {
                settingsRef.AutoWrite = value;
            }
        }

        public int AutoSaveMins
        {
            get
            {
                return settingsRef.AutoSaveMinutes;
            }

            set
            {
                if (value <= 0)
                {
                    AutoSave = false;
                }

                settingsRef.AutoSaveMinutes = value;
            }
        }

        public bool ShowPopupEachSave
        {
            get
            {
                return settingsRef.ShowPopupEachSave;
            }

            set
            {
                settingsRef.ShowPopupEachSave = value;
            }
        }

        public int PopupMinutes
        {
            get
            {
                return settingsRef.PopupMinutes;
            }

            set
            {
                if (value <= 0)
                {
                    ShowPopupWithTimer = false;
                }

                settingsRef.PopupMinutes = value;
            }
        }

        public bool ShowPopupWithTimer
        {
            get
            {
                return settingsRef.ShowPopupWithTimer;
            }

            set
            {
                settingsRef.ShowPopupWithTimer = value;
            }
        }
    }
}
