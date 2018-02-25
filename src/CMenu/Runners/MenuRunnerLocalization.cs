namespace CMenu.Runners
{
    public class MenuRunnerLocalization
    {
        public string OptionIndent { get; set; } = "  ";
        public string OptionDelimiter { get; set; } = " - ";
        public char MenuHeaderBorder { get; set; } = '#';
        public string SubmenuArray { get; set; } = " -->";
        public string ExitOptionValue { get; set; } = "X";
        public string ExitOptionTitle { get; set; } = "Exit";
        public string ActionIsNotDefined { get; set; } = "Action for item is not defined.";
        public string CannotHandleOption { get; set; } = "Cannot handle this option!";
        public string ChooseOption { get; set; } = "Choose option: ";
        public string OptionIsNotValid { get; set; } = "Value \"{0}\" is not valid. Please enter a valid option.";
        public string TryAgainPrompt { get; set; } = ": ";
    }
}