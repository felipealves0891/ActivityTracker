using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Client.ViewModels
{
    public class WorkspaceViewModel : NotifyPropertyChanged
    {
		private bool _loading;
		public bool Loading
		{
			get => _loading;
			set => SetField(ref _loading, value);
		}

        public WorkspaceViewModel()
        {
            Issue = new IssueViewModel(this);
        }

        public IssueViewModel Issue { get; set; }

    }
}
