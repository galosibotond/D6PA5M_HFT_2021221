using D6PA5M_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace D6PA5M_HFT_2021221.WpfClient
{
    public class RecordCompanyViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<RecordCompany> RecordCompanyCollection { get; set; }

        private RecordCompany selectedRecordCompany;

        public RecordCompany SelectedRecordCompany
        {
            get { return selectedRecordCompany; }
            set
            {
                if (value != null)
                {
                    selectedRecordCompany = new RecordCompany()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteRecordCompanyCommand as RelayCommand)?.NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateRecordCompanyCommand { get; set; }

        public ICommand DeleteRecordCompanyCommand { get; set; }

        public ICommand UpdateRecordCompanyCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public RecordCompanyViewModel()
        {
            if (!IsInDesignMode)
            {
                RecordCompanyCollection = new RestCollection<RecordCompany>("http://localhost:36957/", "recordCompany", "hub");
                CreateRecordCompanyCommand = new RelayCommand(() =>
                {
                    RecordCompanyCollection?.Add(new RecordCompany()
                    {
                        Name = SelectedRecordCompany.Name
                    });
                });

                UpdateRecordCompanyCommand = new RelayCommand(() =>
                {
                    try
                    {
                        RecordCompanyCollection?.Update(SelectedRecordCompany);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteRecordCompanyCommand = new RelayCommand(() =>
                {
                    RecordCompanyCollection.Delete(SelectedRecordCompany.Id);
                },
                () =>
                {
                    return SelectedRecordCompany != null;
                });
                SelectedRecordCompany = new RecordCompany();
            }

        }
    }
}
