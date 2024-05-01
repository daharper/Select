using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Select.Presentation.Utils;

/// <summary>
/// Useful extensions for DataGridViews.
/// </summary>
public static class DataGridViews
{
    /// <summary>
    /// Set double buffering on the target grid.
    /// </summary>
    public static void SetDoubleBuffering(this DataGridView targetGrid, bool value)
    {
        var type = targetGrid.GetType();
        var pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)!;

        pi.SetValue(targetGrid, value, null);
    }

    /// <summary>
    /// Resize the columns of the target grid to fit the data quickly.
    /// </summary>
    public static void FastAutoSizeColumns(this DataGridView targetGrid)
    {
        var table = ((DataView)targetGrid.DataSource).Table;

        // Create a graphics object from the target grid. Used for measuring text size.
        using var gfx = targetGrid.CreateGraphics();
        
        // Iterate through the columns.
        for (var i = 0; i < table.Columns.Count; i++)
        {
            // Leverage Linq enumerator to rapidly collect all the rows into a string array, making sure to exclude null values.
            var colStringCollection = table.AsEnumerable().Where(r => r.Field<object>(i) != null).Select(r => r.Field<object>(i).ToString()).ToArray();

            // Sort the string array by string lengths.
            colStringCollection = colStringCollection.OrderBy(x => x.Length).ToArray();

            // Get the last and longest string in the array.
            var longestColString = colStringCollection.Length == 0 ? "" : colStringCollection.Last();

            // Use the graphics object to measure the string size.
            var colWidth = gfx.MeasureString(longestColString, targetGrid.Font);

            // If the calculated width is larger than the column header width, set the new column width.
            if (colWidth.Width > targetGrid.Columns[i].HeaderCell.Size.Width)
            {
                targetGrid.Columns[i].Width = (int)colWidth.Width;
            }
            else // Otherwise, set the column width to the header width.
            {
                targetGrid.Columns[i].Width = targetGrid.Columns[i].HeaderCell.Size.Width;
            }
        }
    }
}
