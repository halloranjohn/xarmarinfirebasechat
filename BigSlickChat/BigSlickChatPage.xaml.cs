using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BigSlickChat
{
	public partial class BigSlickChatPage : ContentPage
	{
		// Define some data.
		public static ObservableCollection<ChatItem> messages = new ObservableCollection<ChatItem>
			//List<ChatItem> people = new List<ChatItem>
			{
				new ChatItem("lsflkdsdj slkdj sakldjaskld asldkjlkas dkjlsa ", "June"),
				new ChatItem("jhadkajshd kjahdajkshdjkas dhjkask dh", "June"),
				new ChatItem("sdjkahsdjkhsadkjhas ", "June"),
				new ChatItem("asdsjlkja dlkdklaskldskdlkajslioprw io", "June")
			};

		public BigSlickChatPage()
		{
			InitializeComponent();

			InitStackLayout();
			InitSidebarButton();
			InitList();
			InitEntry();

			DependencyService.Get<FirebaseService>().ObserveChildEvent<ChatItem>("chat-items", OnChatItemsChildEvent);
			DependencyService.Get<FirebaseService>().ObserveValueEvent<Dictionary<string, ChatItem>>("chat-items", OnChatItemsValueEvent);
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
				//Navigation.PopModalAsync(false);
				Navigation.PushModalAsync(new SidebarPage());
			};
		}

		public void OnChatItemsChildEvent(ChatItem item)
		{
			messages.Add(item);

			list.ScrollTo(messages[messages.Count - 1], ScrollToPosition.End, true);
		}

		public void OnChatItemsValueEvent(Dictionary<string, ChatItem> items)
		{
			foreach (ChatItem item in items.Values)
			{
				messages.Add(item);
			}

			DependencyService.Get<FirebaseService>().RemoveValueEvent<Dictionary<string, ChatItem>>("chat-items");

			list.ScrollTo(messages[messages.Count-1], ScrollToPosition.End, true);
		}

		private void InitEntry()
		{
			entry.BackgroundColor = Color.White;
			entry.VerticalOptions = LayoutOptions.End;
			entry.HorizontalOptions = LayoutOptions.Fill;

			entry.Completed += (sender, e) =>
			{
				DependencyService.Get<FirebaseService>().SetChildValueByAutoId("chat-items", new ChatItem(entry.Text, "15/03/2017"));
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
