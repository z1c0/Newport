$fileNames = Get-ChildItem "..\Newport.Demo.Universal\Newport.Demo.Universal.Shared\*Page.xaml"
foreach ($f in $fileNames) 
{
  write $f.Name;
  $code = Get-Content $f;
  #
  # Patch and write xaml file.
  #
  $code = $code -replace "<Page", "<phone:PhoneApplicationPage`r`n  xmlns:phone=`"clr-namespace:Microsoft.Phone.Controls;assembly=Microsoft.Phone`"`r`n  xmlns:i=`"clr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity`""
  $code = $code -replace "</Page>", "</phone:PhoneApplicationPage>"
  $code = $code -replace "using:Newport.Demo.Universal.ViewModels", "clr-namespace:Newport.Demo.Universal.ViewModels"  
  $code = $code -replace "using:Newport", "clr-namespace:Newport;assembly=Newport.WindowsPhone" 
  $code = $code -replace "n:Interaction.Behaviors", "i:Interaction.Behaviors"
  
  Set-Content $f.Name $code
  #
  # Create xaml.cs file
  #
  $className = $f.BaseName
  $code = 
"namespace Newport.Demo.Universal
{
  public partial class $className
  {
    public $className()
    {
      InitializeComponent();
    }
  }
}"

  Set-Content ($f.Name + ".cs") $code
}