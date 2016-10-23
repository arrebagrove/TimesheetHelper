using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace TimesheetHelper
{
    public class Settings: INotifyPropertyChanged
    { 
        #region Members/Properties

        private string _excelFile;
        [DisplayName("Excel File")]
        public string ExcelFile
        {
            get
            {
                return this._excelFile;
            }

            set
            {
                this._excelFile = value;
                mCalculatedPath = null;
                OnPropertyChanged();
            }
        }

        private string _filePath;        
        public string FilePath
        {
            get
            {
                return this._filePath;
            }

            set
            {
                this._filePath = value;
                mCalculatedPath = null;
                OnPropertyChanged();
            }
        }

        private string _message;
        [DisplayName("Topic")]
        public string Message
        {
            get
            {
                return this._message;
            }

            set
            {
                this._message = value;
                OnPropertyChanged();
            }
        }

        private bool _autoWrite;
        [DisplayName("Auto Write")]
        public bool AutoWrite
        {
            get
            {
                return _autoWrite;
            }

            set
            {
                _autoWrite = value;
                OnPropertyChanged();
            }
        }

        private int _saveMins;
        [DisplayName("Auto Write Timer(min)")]
        public int AutoSaveMinutes
        {
            get
            {
                return this._saveMins;
            }

            set
            {
                if (value <= 0)
                {
                    AutoWrite = false;
                    value = 0;
                }
                this._saveMins = value;
                OnPropertyChanged();
            }
        }

        private bool _showPopupEachSave;
        [DisplayName("Show Popup for each write")]
        public bool ShowPopupEachSave
        {
            get
            {
                return this._showPopupEachSave;
            }

            set
            {
                this._showPopupEachSave = value;
                OnPropertyChanged();
            }
        }

        private int _popupMinutes;
        [DisplayName("Reminder Popup Timer(min)")]
        public int PopupMinutes
        {
            get
            {
                return this._popupMinutes;
            }

            set
            {
                if (value <= 0)
                {
                    ShowPopupWithTimer = false;
                    value = 0;                    
                }

                this._popupMinutes = value;
                OnPropertyChanged();
            }
        }

        private bool _showPopupWithTimer;
        [DisplayName("Reminder Popup")]
        public bool ShowPopupWithTimer
        {
            get
            {
                return this._showPopupWithTimer;
            }

            set
            {                
                this._showPopupWithTimer = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        [DisplayName("Name Surname")]
        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this._name = value;
                OnPropertyChanged();
            }
        }

        private string _team;
        [DisplayName("Team Name")]
        public string Team
        {
            get
            {
                return this._team;
            }

            set
            {
                this._team = value;
                OnPropertyChanged();
            }
        }

        private string _studentID;
        [DisplayName("Student ID")]
        public string StudentID
        {
            get
            {
                return this._studentID;
            }

            set
            {
                this._studentID = value;
                OnPropertyChanged();
            }
        }

        private bool _folderByMonth;
        [DisplayName("Categorize by month")]
        public bool FolderByMonth
        {
            get
            {
                return _folderByMonth;
            }
            set
            {
                this._folderByMonth = value;
                OnPropertyChanged();
            }
        }
        #endregion

        // Non-saved options.
        private string mCalculatedPath;
        public string CalculatedPath
        {
            get
            {
                if (string.IsNullOrEmpty(mCalculatedPath))
                {
                    mCalculatedPath = _calculateFilePath();
                }

                return mCalculatedPath;
            }
        }

        private static Settings _programSettings = null;
        public static Settings CurrentSettings
        {
            get
            {
                if (_programSettings == null)
                {
                    _programSettings = new Settings();
                }

                return _programSettings;
            }

            set
            {
                //_programSettings = value;
                //OnGlobalPropertyChanged(string.Empty);

                // TODO(batuhan): Fix me.
                _programSettings.AutoWrite = value.AutoWrite;
                _programSettings.AutoSaveMinutes = value.AutoSaveMinutes;                
                _programSettings.ExcelFile = value.ExcelFile;
                _programSettings.FilePath = value.FilePath;
                _programSettings.FolderByMonth = value.FolderByMonth;
                _programSettings.Message = value.Message;
                _programSettings.Name = value.Name;
                _programSettings.PopupMinutes = value.PopupMinutes;                
                _programSettings.ShowPopupEachSave = value.ShowPopupEachSave;
                _programSettings.ShowPopupWithTimer = value.ShowPopupWithTimer;
                _programSettings.StudentID = value.StudentID;
                _programSettings.Team = value.Team;
            }
        }

        private string _calculateFilePath()
        {
            if (this.FilePath == null || this.ExcelFile == null)
            {
                throw new NullReferenceException("Null reference on filepath or excel file!");
            }
            else
            {
                string result = this.FilePath;
                if (this.FolderByMonth)
                {
                    int month = System.DateTime.Today.Month;
                    string monthName = _getMonthName(month);
                    result += System.DateTime.Today.Month.ToString("00");
                    result += " ";
                    result += monthName;
                    result += "\\";
                }
                result += this.ExcelFile;
                return result;
            }
        }

        private string _getMonthName(int monthNumber)
        {
            DateTime temp = new DateTime(2010, monthNumber, 10);

            return temp.ToString("MMM", CultureInfo.CurrentCulture);
        }

        public void Rollback()
        {
            CurrentSettings = InitialSettings();
        }

        public Settings()
        {
            // NOTE(batuhan): Weak event handler for static instance.
            GlobalPropertyChanged += this.PropertyChanged;
        }

        public static Settings InitialSettings()
        {
            Settings opt = new Settings();
            DateTime date = System.DateTime.Now;
            opt.ExcelFile = "Timesheet_" + date.Day.ToString() +
                "_" + date.Month.ToString() + "_" + date.Year.ToString();
            opt.FilePath = "timesheet\\";
            opt.FolderByMonth = false;
            opt.Message = "Don't forget to change settings.";
            opt.AutoSaveMinutes = 15;
            opt.ShowPopupEachSave = true;
            opt.PopupMinutes = 30;
            opt.ShowPopupWithTimer = false;
            opt.Name = "Full Name";
            opt.Team = "Team no/name";
            opt.StudentID = "ID";

            return opt;
        }

        public static event PropertyChangedEventHandler GlobalPropertyChanged;
        protected static void OnGlobalPropertyChanged([CallerMemberName] string propertyName = null)
        {
            GlobalPropertyChanged?.Invoke(typeof(Settings), new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
