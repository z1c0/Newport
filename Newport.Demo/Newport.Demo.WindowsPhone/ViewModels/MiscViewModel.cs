using System;
using System.Windows;

namespace Newport
{
  [ExportedViewModel]
  public class MiscViewModel : ViewModelBase
  {
    public MiscViewModel()
    {
      SundayCommand = new ActionCommand(
        o => MessageBox.Show("I only work on Sunday ..."),
        o => DateTime.Today.DayOfWeek == DayOfWeek.Sunday);

      SomeDayCommand = new GenericActionCommand<string>(
        s => MessageBox.Show("I only work on " + s + " ..."),
        s => DateTime.Today.DayOfWeek.ToString() == s);
    }

    public GenericActionCommand<string> SomeDayCommand { get; private set; }

    public ActionCommand SundayCommand { get; private set; }
  }
}


