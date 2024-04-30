using System.Drawing;
using EasyScintilla.Stylers;
using ScintillaNET;

namespace Select.Presentation.Utils;

public class DarkSqlStyler : SqlStyler
{
    private Color _backupCaretForeColor;
    
    public override void ApplyStyle(Scintilla scintilla)
    {
        var backColor = Color.FromArgb(26, 26, 26);

        foreach (var style in scintilla.Styles)
        {
            style.BackColor = backColor;
        }

        scintilla.Styles[Style.Sql.Character].ForeColor = Color.FromArgb(93, 168, 230);
        scintilla.Styles[Style.Sql.Comment].ForeColor = Color.FromArgb(87, 166, 74);
        scintilla.Styles[Style.Sql.CommentLine].ForeColor = Color.FromArgb(87, 166, 74);
        scintilla.Styles[Style.Sql.CommentDoc].ForeColor = Color.FromArgb(87, 166, 74);
        scintilla.Styles[Style.Sql.Number].ForeColor = Color.FromArgb(181, 206, 168);
        scintilla.Styles[Style.Sql.String].ForeColor = Color.FromArgb(214, 157, 133);
        scintilla.Styles[Style.Sql.Operator].ForeColor = Color.FromArgb(220, 220, 220);
        scintilla.Styles[Style.Sql.Identifier].ForeColor = Color.FromArgb(220, 220, 220);
        scintilla.Styles[Style.Sql.Word].ForeColor = Color.FromArgb(201, 130, 181);
        scintilla.Styles[Style.Sql.Word2].ForeColor = Color.FromArgb(93, 168, 230);
        scintilla.Styles[Style.Sql.Default].ForeColor = Color.FromArgb(214, 157, 133);
        scintilla.Styles[Style.Sql.QOperator].ForeColor = Color.FromArgb(252, 156, 108);
        scintilla.Styles[Style.Sql.QuotedIdentifier].ForeColor = Color.FromArgb(252, 156, 108);
        scintilla.Styles[Style.Sql.User1].ForeColor = Color.FromArgb(252, 156, 108);
        scintilla.Styles[Style.Sql.User2].ForeColor = Color.FromArgb(252, 156, 108);
        scintilla.Styles[Style.Sql.User3].ForeColor = Color.FromArgb(252, 156, 108);
        scintilla.Styles[Style.Sql.User4].ForeColor = Color.FromArgb(252, 156, 108);


        scintilla.Styles[Style.Sql.Character].Bold = true;
        scintilla.Styles[Style.Sql.Comment].Italic = true;
        scintilla.Styles[Style.Sql.CommentLine].Italic = true;
        scintilla.Styles[Style.Sql.CommentDoc].Italic = true;
        scintilla.Styles[Style.Sql.Number].Bold = true;
        scintilla.Styles[Style.Sql.String].Bold = true;
        

        scintilla.CaretForeColor = Color.FromArgb(220, 220, 220);

        scintilla.SetFoldMarginHighlightColor(true, backColor);
        scintilla.SetFoldMarginColor(true, backColor);

        scintilla.SetSelectionBackColor(true, Color.FromArgb(38, 79, 120));
    }

    public override void RemoveStyle(Scintilla scintilla)
    {
        base.RemoveStyle(scintilla);

        _backupCaretForeColor = scintilla.CaretForeColor;
        scintilla.CaretForeColor = _backupCaretForeColor;

        scintilla.SetFoldMarginColor(false, Color.Empty);
        scintilla.SetFoldMarginHighlightColor(false, Color.Empty);

        scintilla.SetSelectionBackColor(true, Color.Silver);
    }
}
