using System.Collections.Generic;
using System.Data;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace TaskyAndroid
{
	[Activity (Label = "Data Grid Example", MainLauncher = true, Icon="@drawable/icon")]			
	public class DataGridExampleScreen : Activity
	{
		private DataTable dataTable = null;
		private GridView gridView = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// set our layout to be the home screen
			SetContentView(Resource.Layout.DataGridExampleScreen);

			gridView = FindViewById<GridView> (Resource.Id.offerGridView);

			var txtTitle = FindViewById<TextView> (Resource.Id.txtTitle);
			var txtAddColumn = FindViewById<TextView> (Resource.Id.txtAddColumn);
			var txtAddRow = FindViewById<TextView> (Resource.Id.txtAddRow);

			txtTitle.SetText ("Data Grid Example", TextView.BufferType.Normal);

			txtAddColumn.Click += (sender, e) => {
				dataTable.Columns.Add("New Col " + (dataTable.Columns.Count + 1));
				gridView.NumColumns = dataTable.Columns.Count;
				gridView.InvalidateViews();
			};
			txtAddRow.Click += (sender, e) => {
				var row = dataTable.NewRow();
				dataTable.Rows.Add(row);
				gridView.InvalidateViews();
			};
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			dataTable = new DataTable ();

			dataTable.Columns.Add ("Col 1");
			dataTable.Columns.Add ("Col 2");
			dataTable.Columns.Add ("Col 3");

			for (int i = 0; i < 3; i++) {
				var row = dataTable.NewRow ();
				row[0] = "Fizz " + i;
				row[1] = "Buzz " + i;
				row[2] = "FizzBuzz " + i;
				dataTable.Rows.Add(row);
			}

			// create our adapter
			var itemListAdapter = new Adapters.DataTableGridAdapter (this, dataTable);

			gridView.NumColumns = dataTable.Columns.Count;

			//Hook up our adapter to our ListView
			gridView.Adapter = itemListAdapter;

		}

	}
}

