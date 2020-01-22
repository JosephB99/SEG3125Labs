package com.example.lab1;

import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

import androidx.appcompat.app.AppCompatActivity;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.google.android.material.bottomnavigation.BottomNavigationView;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BottomNavigationView navView = findViewById(R.id.nav_view);
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(
                R.id.navigation_home, R.id.navigation_dashboard, R.id.navigation_notifications)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(navView, navController);


        JSONReader reader = new JSONReader();
        String test = reader.loadJSONFromAsset(getApplicationContext());

        JSONObject obj;
        try {
            obj = new JSONObject(test);
            populateTests(obj);
        } catch (JSONException e) {

        }

    }

    protected void populateTests(JSONObject data) {
        Spinner tests = findViewById(R.id.dropdownTests);
        final List<String> typesOfTests = new ArrayList<String>();

        try {
            JSONObject tests1 = data.getJSONObject("tests");

            for(int i = 0; i < tests1.length(); i++) {
                typesOfTests.add(tests1.getString("name"));
            }

            ArrayAdapter<String> adp1 = new ArrayAdapter<String>(this,
                    android.R.layout.simple_list_item_1, typesOfTests);
            adp1.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            tests.setAdapter(adp1);

        } catch (JSONException e) {

        }
     }

}
