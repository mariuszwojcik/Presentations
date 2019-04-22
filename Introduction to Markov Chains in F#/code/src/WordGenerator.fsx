
let corpora = [| "China";"India";"United States";"Indonesia";"Brazil";"Pakistan";"Nigeria";"Bangladesh";"Russia";"Mexico";"Japan";"Philippines";"Ethiopia";"Egypt";"Vietnam";"Democratic Republic of the Congo";"Germany";"Iran";"Turkey";"France";"Thailand";"United Kingdom";"Italy";"South Africa";"Tanzania";"Myanmar";"Kenya";"South Korea";"Colombia";"Spain";"Argentina";"Algeria";"Ukraine";"Sudan";"Uganda";"Iraq";"Poland";"Canada";"Morocco";"Uzbekistan";"Saudi Arabia";"Malaysia";"Peru";"Venezuela";"Afghanistan";"Ghana";"Angola";"Nepal";"Yemen";"Mozambique";"Ivory Coast";"North Korea";"Australia";"Madagascar";"Cameroon";"Taiwan";"Niger";"Sri Lanka";"Burkina Faso";"Mali";"Romania";"Chile";"Syria";"Kazakhstan";"Guatemala";"Malawi";"Zambia";"Netherlands";"Ecuador";"Cambodia";"Senegal";"Chad";"Somalia";"Zimbabwe";"South Sudan";"Rwanda";"Guinea";"Benin";"Haiti";"Tunisia";"Bolivia";"Belgium";"Cuba";"Burundi";"Greece";"Czech Republic";"Jordan";"Dominican Republic";"Portugal";"Sweden";"Azerbaijan";"Hungary";"United Arab Emirates";"Belarus";"Honduras";"Israel";"Tajikistan";"Austria";"Papua New Guinea";"Switzerland";"Sierra Leone";"Togo";"Hong Kong";"Paraguay";"Laos";"Serbia";"Bulgaria";"El Salvador";"Libya";"Nicaragua";"Kyrgyzstan";"Lebanon";"Turkmenistan";"Denmark";"Singapore";"Republic of the Congo";"Finland";"Central African Republic";"Slovakia";"Norway";"Eritrea";"Costa Rica";"New Zealand";"Ireland";"Palestine";"Oman";"Liberia";"Kuwait";"Panama";"Croatia";"Mauritania";"Georgia";"Moldova";"Uruguay";"Bosnia and Herzegovina";"Mongolia";"Puerto Rico";"Armenia";"Albania";"Lithuania";"Qatar";"Jamaica";"Namibia";"Botswana";"The Gambia";"Gabon";"Slovenia";"North Macedonia";"Lesotho";"Latvia";"Kosovo";"Guinea-Bissau";"Bahrain";"East Timor";"Trinidad and Tobago";"Equatorial Guinea";"Estonia";"Mauritius";"Eswatini";"Djibouti";"Fiji";"Comoros";"Cyprus";"Guyana";"Bhutan";"Solomon Islands";"Macau";"Montenegro";"Luxembourg";"Western Sahara";"Suriname";"Cape Verde";"Malta";"Transnistria";"Brunei";"Belize";"Bahamas";"Maldives";"Iceland";"Northern Cyprus";"Vanuatu";"Barbados";"New Caledonia";"French Polynesia";"Abkhazia";"São Tomé and Príncipe";"Samoa";"Saint Lucia";"Guam";"Curaçao";"Artsakh";"Kiribati";"Aruba";"Saint Vincent and the Grenadines";"Grenada";"Jersey";"Federated States of Micronesia";"United States Virgin Islands";"Antigua and Barbuda";"Tonga";"Seychelles";"Isle of Man";"Andorra";"Dominica";"Bermuda";"Cayman Islands";"Guernsey";"American Samoa";"Northern Mariana Islands";"Saint Kitts and Nevis";"Greenland";"Marshall Islands";"South Ossetia";"Faroe Islands";"Turks and Caicos Islands";"Sint Maarten";"Liechtenstein";"Monaco";"Saint-Martin";"Gibraltar";"San Marino";"British Virgin Islands";"Palau";"Cook Islands";"Anguilla";"Wallis and Futuna";"Nauru";"Tuvalu";"Saint Barthélemy";"Saint Pierre and Miquelon ";"Saint Helena, Ascension";"and Tristan da Cunha";"Montserrat";"Falkland Islands";"Christmas Island";"Norfolk Island";"Niue";"Tokelau";"Vatican City";"Cocos";"Pitcairn Islands"|]


//#region simple Markov chains

let transitions =
    corpora
    |> String.concat "|"
    |> Seq.windowed 2
    |> Seq.groupBy(fun a -> a.[0])
    |> Seq.map(fun (k, e) -> (k,e |> Seq.map(Seq.skip 1 >> Seq.head) |> Seq.toArray))
    |> Map.ofSeq

let r = System.Random()

let nextChar c =
    let items = transitions.[c]
    items.[r.Next(items.Length)]

Seq.unfold(fun s -> match nextChar s with | '|' -> None | c -> Some (s, c)) 'Z' |> Seq.map(string) |> String.concat ""

//#endregion


//#region NGrams

let generateCountryName corpora ngramOrder =
    let transitions =
        corpora
        |> String.concat "|"
        |> Seq.windowed (ngramOrder + 1)
        |> Seq.filter(fun i -> System.Char.IsLetter(i.[0]))
        |> Seq.groupBy (fun a -> System.String( a.[0 .. ngramOrder - 1] ))
        |> Seq.map(fun (k, e) -> (k,e |> Seq.map(fun c -> c |> Seq.skip ngramOrder |> Seq.head) |> Seq.toArray))
        |> Map.ofSeq

    let r = System.Random()

    let chooseRandom (items : 'a array) =
        items.[r.Next(items.Length)]

    let beginning = 
        transitions 
        |> Map.toArray 
        |> Array.map(fun (from, _) -> from) 
        |> Array.filter(fun i -> System.Text.RegularExpressions.Regex.IsMatch(i, "^[A-Z].*[a-z]$"))
        |> chooseRandom

    let nextChar c =
        let items = transitions.TryFind c
        items |> Option.map(chooseRandom >> string) |> Option.defaultValue " "

    let rec generate (state : string) =

        if (state.Length > 200) then state
        else
            let lastNGram = state.Substring(state.Length - ngramOrder, ngramOrder)
            match (nextChar lastNGram) with
            | "|" -> state
            | c -> generate (state + c)

    generate beginning

generateCountryName corpora 5

//#endregion
