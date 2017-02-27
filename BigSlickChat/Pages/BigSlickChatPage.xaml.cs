using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Linq;

namespace BigSlickChat
{
	public partial class BigSlickChatPage : ContentPage
	{
        public const string MESSAGES_URL_PREFIX = "messages/";
        public const string MESSAGES_URL_SUFFIX = "/chatitems";

		private string nodePath;

		private ObservableCollection<ChatItem> messages = new ObservableCollection<ChatItem>();

		public BigSlickChatPage(string channelId)
		{
			InitializeComponent();

            nodePath = string.Format("{0}{1}{2}", MESSAGES_URL_PREFIX, channelId, MESSAGES_URL_SUFFIX);
			InitStackLayout();
			InitSidebarButton();
			InitList();
			InitEntry();
			InitAddBtn();

            DependencyService.Get<FirebaseDatabaseService>().AddChildEvent<ChatItem>(nodePath, OnChatItemAdded, OnChatItemRemoved, OnChatItemChanged);
            //DependencyService.Get<FirebaseDatabaseService>().AddSingleValueEvent<Dictionary<string, ChatItem>>(nodeKey, SetupChatItems);
		}

		//~BigSlickChatPage()
		//{
		//	Debug.WriteLine("Chat page deleted");
		//}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			DependencyService.Get<FirebaseDatabaseService>().RemoveChildEvent(nodePath);
			//DependencyService.Get<FirebaseDatabaseService>().RemoveValueEvent(nodeKey);
		}

		private void InitStackLayout()
		{
			stackLayout.Orientation = StackOrientation.Vertical;
			stackLayout.HeightRequest = 1000;
			stackLayout.HorizontalOptions = LayoutOptions.Fill;
			stackLayout.VerticalOptions = LayoutOptions.FillAndExpand;
			stackLayout.BackgroundColor = Color.FromHex(UserService.Instance.User.color);
		}

		private void InitSidebarButton()
		{
			sidebarButton.Text = "Goto Sidebar";
			sidebarButton.HorizontalOptions = LayoutOptions.Fill;

			sidebarButton.Clicked += (sender, e) =>
			{
				Navigation.PopAsync();
				//ChangePage(new SidebarPage());
			};
		}

		private void InitAddBtn()
		{
			int numToAdd = 10;
			addBtn.Text = "Add " + numToAdd + " Random messages";
			addBtn.HorizontalOptions = LayoutOptions.Fill;

			addBtn.Clicked += (sender, e) =>
			{
				Random r = new Random();
				const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				
				string randText;
				int strLength = 10;
				char[] cs;
				ChatItem chat;
				for(int i = 0; i < numToAdd; i++)
				{
					cs = Enumerable.Repeat(chars, strLength).Select(s => s[r.Next(s.Length)]).ToArray();
					randText = new string(cs);
					chat = new ChatItem(randText, "15/03/2017");
					string newKey = FirebaseDatabaseSharedService.Instance.BatchSetObj(nodePath, chat);
					chat.Key = newKey;
				}
			};
		}

		private async Task ChangePage(Page newPage)
		{
			await Navigation.PushAsync(newPage);
			Debug.WriteLine("DODODOD IT");
			Navigation.RemovePage(this);
		}

		private void InitEntry()
		{
            entry.Placeholder = "Type Message...";
			entry.BackgroundColor = Color.White;
			entry.VerticalOptions = LayoutOptions.End;
			entry.HorizontalOptions = LayoutOptions.Fill;

			entry.Completed += (sender, e) =>
			{
				ChatItem chat = new ChatItem(entry.Text, "15/03/2017");
				//chat.Key = DependencyService.Get<FirebaseDatabaseService>().SetChildValueByAutoId(nodePath, chat);
				chat.Key = FirebaseDatabaseSharedService.Instance.BatchSetObj(nodePath, chat);
			};
		}


		private void InitList()
		{
			list.VerticalOptions = LayoutOptions.FillAndExpand;
			list.HorizontalOptions = LayoutOptions.FillAndExpand;
			list.BackgroundColor = Color.White;

			// Source of data items.
			list.ItemsSource = messages;

			// Define template for displaying each item.
			// (Argument of DataTemplate constructor is called for 
			//      each item; it must return a Cell derivative.)
			list.ItemTemplate = new DataTemplate(() =>
			{
				// Create views with bindings for displaying each property.
				Label messageLabel = new Label();
				messageLabel.SetBinding(Label.TextProperty, "Message");

				Label dateLabel = new Label();
				dateLabel.SetBinding(Label.TextProperty,
					new Binding("Date", BindingMode.OneWay,
				                null, null, "Date {0:d}"));
				
				BoxView boxView = new BoxView();
				boxView.Color = Color.Gray;

				// Return an assembled ViewCell.
				return new ViewCell
				{
					View = new StackLayout
					{
						Padding = new Thickness(0, 5),
						Orientation = StackOrientation.Horizontal,
						Children =
						{
								boxView,
								new StackLayout
								{
									VerticalOptions = LayoutOptions.Center,
									Spacing = 0,
									Children =
									{
										messageLabel,
										dateLabel
									}
								}
						}
					}
				};
			});

			list.ItemSelected += (sender, e) =>
			{
				DeleteChat(sender, e);
			};
		}

		void DeleteChat(object sender, SelectedItemChangedEventArgs e)
		{
			ChatItem chatItem = e.SelectedItem as ChatItem;

			if(chatItem.Key != null)
			{
				string nodePath = string.Format("{0}{1}{2}", MESSAGES_URL_PREFIX, "Bri2", MESSAGES_URL_SUFFIX);
				FirebaseDatabaseSharedService.Instance.BatchRemoveObj(nodePath, chatItem.Key, chatItem);
			}
			else 
			{
				Debug.WriteLine("Error: Cant delete chat, No Firebase key for: " + chatItem.Message.ToString());
			}

			messages.Remove(chatItem);
		}

		public void OnChatItemAdded(string key, ChatItem item)
		{
			item.Key = key;

			messages.Add(item);

			list.ScrollTo(messages[messages.Count - 1], ScrollToPosition.End, true);

            entry.Text = null;
		}

		public void OnChatItemRemoved(string key, ChatItem item)
		{
			// TODO: Implement
		}

		public void OnChatItemChanged(string key, ChatItem item)
		{
			// TODO: Implement
		}

        public void SetupChatItems(Dictionary<string, ChatItem> items)
		{
			foreach (ChatItem item in items.Values)
			{
				messages.Add(item);
			}

			list.ScrollTo(messages[messages.Count - 1], ScrollToPosition.End, true);
        }
	}
}
