namespace IGS.Domain.ViewModels.Settings
{
	public class SettingsViewModel
	{
		enum Component
		{
			Profile,
			Confidentiality,
			Safety,
			DeleteAccount
		}

		public string? ComponentName { get; private set; }

		public SettingsViewModel(int id)
		{
			Component component = (Component)id;
			ComponentName = component.ToString();
		}

	}
}
