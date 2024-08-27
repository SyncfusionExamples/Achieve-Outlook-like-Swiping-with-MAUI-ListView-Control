using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookSample
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<InboxInfo> inboxInfos; 
        private ObservableCollection<InboxInfo> archivedMessages;
        private Command deleteCommand;
        private bool? isDeleted;
        private InboxInfo listViewItem;
        private Command archiveCommand;
        private int listViewItemIndex;
        private Command undoCommand;
        private string popUpText;

        public ViewModel() 
        {
            IsDeleted = false;
            archivedMessages = new ObservableCollection<InboxInfo>();
            inboxInfos = GetInboxInfo();
            deleteCommand = new Command(OnDelete);
            archiveCommand = new Command(OnArchive);
            undoCommand = new Command(OnUndo);
        }

        public ObservableCollection<InboxInfo> InboxInfos
        {
            get { return inboxInfos; }
            set { inboxInfos = value; OnPropertyChanged("InboxInfos"); }
        }

        public ObservableCollection<InboxInfo> ArchivedMessages
        {
            get { return archivedMessages; }
            set { archivedMessages = value; OnPropertyChanged("ArchivedMessages"); }
        }

        public Command DeleteCommand
        {
            get { return deleteCommand; }
            protected set { deleteCommand = value; }
        }

        public Command ArchiveCommand
        {
            get { return archiveCommand; }
            protected set { archiveCommand = value; }
        }

        public bool? IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; OnPropertyChanged("IsDeleted"); }
        }

        public string PopUpText
        {
            get { return popUpText; }
            set { popUpText = value; OnPropertyChanged("PopUpText"); }
        }

        public Command UndoCommand
        {
            get { return undoCommand; }
            protected set { undoCommand = value; }
        }

        private async void OnDelete(object item)
        {
            PopUpText = "Deleted";
            listViewItem = (InboxInfo)item;
            listViewItemIndex = inboxInfos.IndexOf(listViewItem);
            inboxInfos!.Remove(listViewItem);
            IsDeleted = true;
            await Task.Delay(3000);
            IsDeleted = false;
        }

        private async void OnArchive(object item)
        {
            PopUpText = "Archived";
            listViewItem = (InboxInfo)item;
            listViewItemIndex = inboxInfos.IndexOf(listViewItem);
            inboxInfos!.Remove(listViewItem);
            archivedMessages!.Add(listViewItem);
            IsDeleted = true;
            await Task.Delay(3000);
            IsDeleted = false;
        }

        private void OnUndo()
        {
            IsDeleted = false;
            if (listViewItem != null)
            {
                inboxInfos!.Insert(listViewItemIndex, listViewItem);
                var archivedItem = archivedMessages.Where(x => x.Name.Equals(listViewItem.Name));

                if (archivedItem != null)
                {
                    foreach (var item in archivedItem)
                    {
                        archivedMessages.Remove(listViewItem);
                        break;
                    }
                }
            }
            listViewItemIndex = 0;
            listViewItem = null;
        }

        internal ObservableCollection<InboxInfo> GetInboxInfo()
        {
            var messages = new ObservableCollection<InboxInfo>();
            int images = 0;
            for (int i = 0; i < Subject.Count(); i++)
            {
                if (images > 5)
                {
                    images = 0;
                }
                var message = new InboxInfo()
                {
                    ProfileName = ProfileList[i],
                    Name = NameList[i],
                    Subject = Subject[i],
                    Description = Descriptions[i],
                    Image = Images[images],
                    IsAttached = Attachments[i],
                    IsImportant = Importants[i],
                };
                messages.Add(message);
                images++;
            }
            return messages;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        string[] ProfileList = new string[]
        {
            "M",
            "MV",
            "MV",
            "T",
            "M",
            "LI",
            "M",
            "M",
            "SO",
            "OT",
            "MO",
            "MA",
            "BT",
            "M",
            "M",
        };

        string[] NameList = new string[]
        {
            "Microsoft",
            "Microsoft Viva",
            "Microsoft Viva",
            "Twitter",
            "Microsoft",
            "LinkedIn",
            "Microsoft",
            "Microsoft",
            "Stack Overflow",
            "Outlook Team",
            "Microsoft Outlook",
            "My Analytics",
            "Blog Team Site",
            "Microsoft",
            "Microsoft",
        };

        string[] Images = new string[]
        {
            "bluecircle.png",
            "greencircle.png",
            "lightbluecircle.png",
            "redcircle.png",
            "violetcircle.png",
            "yellowcircle.png",
        };

        bool[] Attachments = new bool[]
        {
            false,
            false,
            false,
            true,
            false,
            true,
            false,
            true,
            true,
            false,
            false,
            true,
            false,
            true,
            false,
        };

        bool[] Importants = new bool[]
        {
            false,
            true,
            false,
            false,
            false,
            false,
            true,
            false,
            false,
            true,
            true,
            false,
            true,
            false,
            false,
        };

        string[] Subject = new string[]
        {
            "Dev Essentials: Learn about the future of .NET and celebrate Visual Studio's 25th anniversary",
            "Your daily briefing",
            "Your digest email",
            "Be more recognizable",
            "Dev Essentials: Announcing .NET Multiplatform App UI is generally available",
            "You have two new messages",
            "Start learning .NET MAUI and discover a new AI pair programmer",
            "Dev Essentials: Learn how to code with Java",
            "Your friendly, fear-free guide to getting started",
            "Discover what’s new in Outlook",
            "Microsoft Outlook Test Message",
            "My Analytics | Collaboration Edition",
            "You've joined the Blog Team Site group",
            "Microsoft .NET News: Get started with .NET 9.0 and watch sessions from .NET Conf 2024 on demand",
            "Microsoft .NET News: Learn about new tools and updates for .NET developers",
        };

        string[] Descriptions = new string[] {
            "Developer news, updates, and training resources",
            "Dear developer, It's almost the end if the week",
            "Dear developer, discover trends in your work habits",
            "Stand out with a profile photo",
            "On codebase, many platforms: .NET Multiplatform App UI is generally available",
            "You have two new messages",
            "Explore resources to get started with .NET MAUI",
            "Get started: Java for beginners",
            "How to learn and share on Stack Overflow",
            "Hello and welcome to Outlook",
            "This is an email message sent automatically by Microsoft Outlook while testing settings of your account",
            "Discover your habits. Work smarter.",
            "Welcome to the Blog Team Site group",
            "The Xamarin Newsletter is now .NET News",
            "Now available: Visual Studio 2024 version 17.11.0",
        };
    }
}
