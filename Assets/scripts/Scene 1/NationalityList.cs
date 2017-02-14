using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Fungus;

public class NationalityList : MonoBehaviour {
	public Flowchart flowchart;
	private Dropdown nationalityDropdown;
	private List<string> nationalityList= new List<string>();
	private string[] nationalityArray = {
		"United States",
		"Albania",
		"Algeria",
		"Andorra",
		"Angola",
		"Antigua and Barbuda",
		"Argentina",
		"Armenia",
		"Australia",
		"Austria",
		"Azerbaijan",
		"Bahamas",
		"Bahrain",
		"Bangladesh",
		"Barbados",
		"Belarus",
		"Belgium",
		"Belize",
		"Benin",
		"Bhutan",
		"Bolivia",
		"Bosnia and Herzegovina",
		"Botswana",
		"Brazil",
		"Brunei Darussalam",
		"Bulgaria",
		"Burkina Faso",
		"Burundi",
		"Cabo Verde",
		"Cambodia",
		"Cameroon",
		"Canada",
		"Central African Republic",
		"Chad",
		"Chile",
		"China",
		"Colombia",
		"Comoros",
		"Congo",
		"Costa Rica",
		"Côte d'Ivoire",
		"Croatia",
		"Cuba",
		"Cyprus",
		"Czech Republic",
		"Democratic Republic of the Congo",
		"Denmark",
		"Djibouti",
		"Dominica",
		"Dominican Republic",
		"Ecuador",
		"Egypt",
		"El Salvador",
		"Equatorial Guinea",
		"Eritrea",
		"Estonia",
		"Ethiopia",
		"Fiji",
		"Finland",
		"France",
		"Gabon",
		"Gambia",
		"Georgia",
		"Germany",
		"Ghana",
		"Greece",
		"Grenada",
		"Guatemala",
		"Guinea",
		"Guinea-Bissau",
		"Guyana",
		"Haiti",
		"Honduras",
		"Hungary",
		"Iceland",
		"India",
		"Indonesia",
		"Iran",
		"Iraq",
		"Ireland",
		"Israel",
		"Italy",
		"Jamaica",
		"Japan",
		"Jordan",
		"Kazakhstan",
		"Kenya",
		"Kiribati",
		"Kuwait",
		"Kyrgyzstan",
		"Lao People's Democratic Republic (Laos)",
		"Latvia",
		"Lebanon",
		"Lesotho",
		"Liberia",
		"Libya",
		"Liechtenstein",
		"Lithuania",
		"Luxembourg",
		"Macedonia",
		"Madagascar",
		"Malawi",
		"Malaysia",
		"Maldives",
		"Mali",
		"Malta",
		"Marshall Islands",
		"Mauritania",
		"Mauritius",
		"Mexico",
		"Micronesia (Federated States of)",
		"Monaco",
		"Mongolia",
		"Montenegro",
		"Morocco",
		"Mozambique",
		"Myanmar",
		"Namibia",
		"Nauru",
		"Nepal",
		"Netherlands",
		"New Zealand",
		"Nicaragua",
		"Niger",
		"Nigeria",
		"North Korea",
		"Norway",
		"Oman",
		"Pakistan",
		"Palau",
		"Panama",
		"Papua New Guinea",
		"Paraguay",
		"Peru",
		"Philippines",
		"Poland",
		"Portugal",
		"Qatar",
		"Republic of Korea (South Korea)",
		"Republic of Moldova",
		"Romania",
		"Russian Federation",
		"Rwanda",
		"Saint Kitts and Nevis",
		"Saint Lucia",
		"Saint Vincent and the Grenadines",
		"Samoa",
		"San Marino",
		"Sao Tome and Principe",
		"Saudi Arabia",
		"Senegal",
		"Serbia",
		"Seychelles",
		"Sierra Leone",
		"Singapore",
		"Slovakia",
		"Slovenia",
		"Solomon Islands",
		"Somalia",
		"South Africa",
		"South Sudan",
		"Spain",
		"Sri Lanka",
		"Sudan",
		"Suriname",
		"Swaziland",
		"Sweden",
		"Switzerland",
		"Syrian Arab Republic",
		"Tajikistan",
		"Thailand",
		"Timor-Leste",
		"Togo",
		"Tonga",
		"Trinidad and Tobago",
		"Tunisia",
		"Turkey",
		"Turkmenistan",
		"Tuvalu",
		"Uganda",
		"Ukraine",
		"United Arab Emirates",
		"United Kingdom of Great Britain and Northern Ireland",
		"United Republic of Tanzania",
		"Uruguay",
		"Uzbekistan",
		"Vanuatu",
		"Venezuela",
		"Vietnam",
		"Yemen",
		"Zambia",
		"Zimbabwe"
	};
		
	// Use this for initialization
	void Start () {
		// Add all items in array to the new list
		foreach (string nation in nationalityArray) {
			nationalityList.Add (nation);
		}
		nationalityDropdown = gameObject.GetComponent<Dropdown>();
		nationalityDropdown.AddOptions (nationalityList);
	}

	/* 
	 * Grab the index of the nationality listed in the dropdown and return the appropriate 
	 * This method should be attached to onClick button and called when the Immigration form is submitted
	 * */
	public void nationalitySelected () {
		nationalityDropdown = gameObject.GetComponent<Dropdown> ();
		// -1 because the dropdown adds "Select" as the 0 index and so US is pushed to 1 
		// In the array, US is 0 so adjustments need to be made.
		int nationalityDropdownKey = nationalityDropdown.value;
		string nationality;
		// If the user did not make a choice (left it as select, then keyValue =0 
		if (nationalityDropdownKey == 0) {
			nationality = "none"; 
			// Change background color of nationality dropdown to signal need to answer
			nationalityDropdown.GetComponent<Image>().color = Color.red;
		} else {
			nationality = nationalityArray [nationalityDropdown.value - 1];
		}
		// Set the value in Fungus
		flowchart.SetStringVariable ("nationality", nationality); 
	}


}
