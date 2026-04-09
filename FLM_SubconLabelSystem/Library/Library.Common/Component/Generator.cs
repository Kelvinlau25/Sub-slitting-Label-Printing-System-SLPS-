using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Library.common
{
public class Generator : ICollection<FieldSet>
{
    #region Properties
    private DataTable _data;
    /// <summary>
    /// Data Source
    /// </summary>
    public DataTable Data
    {
        get { return _data; }
        set { _data = value; }
    }

    private List<FieldSet> _setting = new List<FieldSet>();
    /// <summary>
    /// Field Setting Collection
    /// </summary>
    public List<FieldSet> Setting
    {
        get { return _setting; }
    }
    #endregion

    /// <summary>
    /// initial the data
    /// </summary>
    public Generator(DataTable data)
    {
        this._data = data;
        this._setting = new List<FieldSet>();
    }

    public void AddField(string Field, string Title, EnumLib.DataType Type)
    {
        this._setting.Add(new FieldSet(Field, Title, Type));
    }

    public void Add(FieldSet item)
    {
        this._setting.Add(item);
    }

    /// <summary>
    /// Generate the HTML table from the DataTable
    /// </summary>
    public override string ToString()
    {
        using (StringWriter _sw = new StringWriter())
        {
            int _counter = -1;

            foreach (FieldSet Itm in this._setting)
            {
                _counter += 1;
                this._data.Columns[Itm.Field].SetOrdinal(_counter);
                this._data.Columns[Itm.Field].AllowDBNull = true;
                this._data.Columns[Itm.Field].ColumnName = Itm.Title;
            }

            _counter += 1;
            short _totalColumnCount = (short)(this.Data.Columns.Count - 1);
            for (int temp = _counter; temp <= _totalColumnCount; temp++)
            {
                this.Data.Columns.RemoveAt(_counter);
            }

            // Render as simple HTML table
            _sw.Write("<table>");
            _sw.Write("<tr>");
            foreach (DataColumn col in this.Data.Columns)
            {
                _sw.Write("<td>" + System.Net.WebUtility.HtmlEncode(col.ColumnName) + "</td>");
            }
            _sw.Write("</tr>");
            foreach (DataRow row in this.Data.Rows)
            {
                _sw.Write("<tr>");
                foreach (DataColumn col in this.Data.Columns)
                {
                    string val = row[col] == null ? string.Empty : row[col].ToString();
                    _sw.Write("<td>" + System.Net.WebUtility.HtmlEncode(val) + "</td>");
                }
                _sw.Write("</tr>");
            }
            _sw.Write("</table>");

            return _sw.ToString();
        }
    }

    public void Clear()
    {
        this._setting.Clear();
    }

    public bool Contains(FieldSet item)
    {
        return this._setting.Contains(item);
    }

    public void CopyTo(FieldSet[] array, int arrayIndex)
    {
        this._setting.CopyTo(array, arrayIndex);
    }

    public int Count
    {
        get { return this._setting.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool Remove(FieldSet item)
    {
        return this._setting.Remove(item);
    }

    public int IndexOf(FieldSet item)
    {
        return this._setting.IndexOf(item);
    }

    public void Insert(int index, FieldSet item)
    {
        this._setting.Insert(index, item);
    }

    public FieldSet this[int index]
    {
        get { return this._setting[index]; }
        set { this._setting[index] = value; }
    }

    public void RemoveAt(int index)
    {
        this._setting.RemoveAt(index);
    }

    public IEnumerator<FieldSet> GetEnumerator()
    {
        return this._setting.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this._setting.GetEnumerator();
    }
}
}
