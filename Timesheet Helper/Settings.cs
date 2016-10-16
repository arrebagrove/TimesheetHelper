using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimesheetHelper
{
    public class Settings: INotifyPropertyChanged
    {
        #region Members/Properties
        private string _excelFile;
        public string ExcelFile
        {
            get
            {
                return this._excelFile;
            }

            set
            {
                this._excelFile = value;
                OnPropertyChanged("ExcelFile");
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
                OnPropertyChanged("FilePath");
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return this._message;
            }

            set
            {
                this._message = value;
                OnPropertyChanged("Message");
            }
        }

        private int _saveMins;
        public int AutoSaveMinutes
        {
            get
            {
                return this._saveMins;
            }

            set
            {
                this._saveMins = value;
                OnPropertyChanged("AutoSaveMinutes");
            }
        }

        private bool _showPopupEachSave;
        public bool ShowPopupEachSave
        {
            get
            {
                return this._showPopupEachSave;
            }

            set
            {
                this._showPopupEachSave = value;
                OnPropertyChanged("ShowPopupEachSave");
            }
        }

        private int _popupMinutes;
        public int PopupMinutes
        {
            get
            {
                return this._popupMinutes;
            }

            set
            {
                this._popupMinutes = value;
                OnPropertyChanged("PopupMinutes");
            }
        }

        private bool _showPopupWithTimer;
        public bool ShowPopupWithTimer
        {
            get
            {
                return this._showPopupWithTimer;
            }

            set
            {
                this._showPopupWithTimer = value;
                OnPropertyChanged("ShowPopupWithTimer");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this._name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return this._surname;
            }

            set
            {
                this._surname = value;
                OnPropertyChanged("Surname");
            }
        }

        private string _studentID;
        public string StudentID
        {
            get
            {
                return this._studentID;
            }

            set
            {
                this._studentID = value;
                OnPropertyChanged("StudentID");
            }
        }

        private bool _folderByMonth;
        public bool FolderByMonth
        {
            get
            {
                return _folderByMonth;
            }
            set
            {
                this._folderByMonth = value;
                OnPropertyChanged("FolderByMonth");
            }
        }
        #endregion

        // TODO(batuhan): Para karsiliginda otomatik timesheet doldurma ac aylik? konu bulmak zor.
        // Non-saved options.
        private string mCalculatedPath;

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
                _programSettings.Surname = value.Surname;
            }
        }

        public string CalculatedPath()
        {
            if (string.IsNullOrEmpty(mCalculatedPath))
            {
                mCalculatedPath = _calculateFilePath();
            }

            return mCalculatedPath;
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
            opt.Name = "Name";
            opt.Surname = "Surname";
            opt.StudentID = "ID";

            return opt;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
