using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Client.ViewModels
{
    public class IssueViewModel : NotifyPropertyChanged
    {
		private readonly WorkspaceViewModel _workspace;

		private string _name;
		public string Name
		{
			get => _name;
			set => SetField(ref _name, value);
		}

		private DateTime? _dueDate;
		public DateTime? DueDate
		{
			get => _dueDate;
			set => SetField(ref _dueDate, value);
		}

		private string _description;
		public string Description
		{
			get => _description;
			set => SetField(ref _description, value);
		}

        public IssueViewModel(WorkspaceViewModel workspace)
        {
			_workspace = workspace;
			_name = string.Empty;
			_description = string.Empty;
        }

		public bool IsFill 
			=> !string.IsNullOrEmpty(_description)
			&& !string.IsNullOrEmpty(_name)
			&& _dueDate is not null;

    }
}
