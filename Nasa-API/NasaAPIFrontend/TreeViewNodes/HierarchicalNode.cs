using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NasaAPIFrontend
{
    public class HierarchicalNode : ObservableObject
    {
        private ObservableCollection<HierarchicalNode> mChildren;
        private string mName;
        private string mValue;
        private bool mIsExpanded;

        public string Name
        {
            get 
            { 
                return mName; 
            }
            set
            {
                if (!string.Equals(mName, value, StringComparison.OrdinalIgnoreCase))
                {
                    mName = value;
                    this.NotifyPropertyChanged(nameof(this.Name));
                }
            }
        }

        public string Value
        {
            get 
            { 
                return mValue; 
            }
            set
            {
                if (!string.Equals(mValue, value, StringComparison.OrdinalIgnoreCase))
                {
                    mValue = value;
                    this.NotifyPropertyChanged(nameof(this.Value));
                }
            }
        }

        public bool IsExpanded
        {
            get 
            { 
                return mIsExpanded; 
            }
            set
            {
                if (mIsExpanded != value)
                {
                    mIsExpanded = value;
                    this.NotifyPropertyChanged(nameof(this.IsExpanded));
                }
            }
        }

        public IList<HierarchicalNode> Children => mChildren;

        public HierarchicalNode Parent { get; private set; }

        public HierarchicalNode(HierarchicalNode parent = null)
        {
            mChildren = new ObservableCollection<HierarchicalNode>();
            this.Parent = parent;
        }
    }
}
