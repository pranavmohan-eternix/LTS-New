namespace LTS.UI.ViewModels;

public abstract class EquipmentItemViewModel
{
    public string Identifier { get; }

    protected EquipmentItemViewModel(string identifier)
    {
        Identifier = identifier;
    }
}