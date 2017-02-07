using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigSlickChat
{
	public partial class BigSlickChatPage : ContentPage
	{
		private string nodeKey;

		private ObservableCollection<ChatItem> messages = new ObservableCollection<ChatItem>();

		public BigSlickChatPage(string channelId)
		{
			InitializeComponent();

			nodeKey = channelId + "/chat-items";
			InitStackLayout();
			InitSidebarButton();
			InitList();
			InitEntry();

			DependencyService.Get<FirebaseDatabaseService>().AddChildEvent<ChatItem>(nodeKey, OnChatItemAdded, OnChatItemRemoved, OnChatItemChanged);
			DependencyService.Get<FirebaseDatabaseService>().AddValueEvent<Dictionary<string, ChatItem>>(nodeKey, OnChatItemsValueEvent);
		}

		//~BigSlickChatPage()
		//{
		//	Debug.WriteLine("Chat page deleted");
		//}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			DependencyService.Get<FirebaseDatabaseService>().RemoveChildEvent(nodeKey);
			DependencyService.Get<FirebaseDatabaseService>().RemoveValueEvent(nodeKey);
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
				Navigation.PopModalAsync(false);
				Navigation.PushModalAsync(new SidebarPage());
			};
		}

		 

		public void OnChatItemAdded(ChatItem item)
		{
			messages.Add(item);

			list.ScrollTo(messages[messages.Count - 1], ScrollToPosition.End, true);
		}

		public void OnChatItemRemoved(ChatItem item)
		{
			// TODO: Implement
		}

		public void OnChatItemChanged(ChatItem item)
		{
			// TODO: Implement
		}

		public void OnChatItemsValueEvent(Dictionary<string, ChatItem> items)
		{
			foreach (ChatItem item in items.Values)
			{
				messages.Add(item);
			}

			DependencyService.Get<FirebaseDatabaseService>().RemoveValueEvent(nodeKey);

			list.ScrollTo(messages[messages.Count-1], ScrollToPosition.End, true);
		}

		private void InitEntry()
		{
			entry.BackgroundColor = Color.White;
			entry.VerticalOptions = LayoutOptions.End;
			entry.HorizontalOptions = LayoutOptions.Fill;

			entry.Completed += (sender, e) =>
			{
				DependencyService.Get<FirebaseDatabaseService>().SetChildValueByAutoId(nodeKey, new ChatItem(entry.Text, "15/03/2017"));
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
		}
	}
}
