namespace Library.common
{
public class FieldSet
{
    public FieldSet(string Field, string Title, EnumLib.DataType Type)
    {
        this.Field = Field;
        this.Title = Title;
        this.Type = Type;
    }

    #region Properties
    private string _Field = string.Empty;
    public string Field
    {
        get { return _Field; }
        set { _Field = value; }
    }

    private string _title = string.Empty;
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    public EnumLib.DataType _type = EnumLib.DataType.String;
    public EnumLib.DataType Type
    {
        get { return _type; }
        set { _type = value; }
    }
    #endregion
}
}
