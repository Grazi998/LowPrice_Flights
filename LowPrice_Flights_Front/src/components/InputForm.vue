<!--eslint-disable-->
<template>
    <form action="" name="flightSearch" class="input_form">
        <div class="form_group">
            <div class="form_item">
                <label class="form_item_label">Origin: </label>
                <AirportInputVue id="originAirport" name="originAirport" @getSelectedValue="getValue($event)" />
            </div>
            <div class="form_item">
                <label class="form_item_label">Destination: </label>
                <AirportInputVue id="destinationAirport" name="destinationAirport"
                    @getSelectedValue="getValue($event)" />
            </div>
        </div>
        <div class="form_group">
            <div class="form_item">
                <label class="form_item_label">Departure date: </label>
                <input v-model="departureDate" name="departureDate" type="date" />
            </div>
            <div class="form_item">
                <label class="form_item_label">Return date: </label>
                <input v-model="returnDate" name="returnDate" type="date" />
            </div>
        </div>
        <div class="form_group">
            <div class="form_item">
                <label class="form_item_label">Passangers: </label>
                <select v-model="passangersNumber">
                    <option value="1" selected>1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                    <option value="6">6</option>
                </select>
            </div>
            <div class="form_item">
                <label class="form_item_label">Currency: </label>
                <select v-model="currencyCode">
                    <option value="EUR" selected>EUR</option>
                    <option value="USD">USD</option>
                    <option value="GBP">GBP</option>
                    <option value="AUD">AUD</option>
                </select>
            </div>
        </div>
        <div class="form_button">
            <input type="button" value="Submit" @click="submitInputs">
        </div>
    </form>
    <FlightTableVue :flight-data="resultFlightsInfo" />
</template>

<script>
import { ref } from 'vue'
import AirportInputVue from './AirportInput.vue';
import FlightTableVue from './FlightTable.vue';
import '../helpers/validation.js'
import * as LSM from '../helpers/LocalStorageManipulation.js'

export default {
    setup() {
        const originAirport = ref("")
        const destinationAirport = ref("")
        const departureDate = ref("")
        const returnDate = ref("")
        const passangersNumber = ref(1)
        const currencyCode = ref("EUR")


        const resultFlightsInfo = ref([]);

        const submitInputs = (e) => {
            $(".loader_wrapper").show();
            var form = $(".input_form");
            if (!form.valid()) {
                e.preventDefault();
                $(".loader_wrapper").hide();

                return;
            }

            const searchParams = {
                OriginAirport: originAirport.value,
                DestinationAirport: destinationAirport.value,
                DepartureDate: departureDate.value,
                ReturnDate: returnDate.value,
                PassangersNumber: passangersNumber.value,
                CurrencyCode: currencyCode.value
            }
            console.log(searchParams)

            if (!LSM.CheckSearchParams(searchParams)) {
                $.ajax({
                    type: "post",
                    url: "/api/Flights/GetFlights",
                    data: JSON.stringify(searchParams),
                    contentType: "application/json",
                    success: function (data) {
                        $(".loader_wrapper").hide();
                        resultFlightsInfo.value = data["data"];
                        LSM.SaveSearchResult(searchParams, data["data"])
                    },
                    error: function (err) {
                        $(".loader_wrapper").hide();
                        console.log(err)
                    }
                })
            }
            else {
                $(".loader_wrapper").hide();
                resultFlightsInfo.value = LSM.GetSearchResult(searchParams)
            }


        }

        function GetIATAFromValue(airportInputValue) {
            var IATACode = airportInputValue.split('-')
            if (IATACode.length == 2) {
                return IATACode[1].trim()
            }
            return ""
        }

        const getValue = (airportInput) => {
            if (airportInput.id === "originAirport") {
                const validValue = GetIATAFromValue(airportInput.value)

                if (validValue != "") {
                    $("#originAirport").val(airportInput.value)
                }
                else {
                    $("#originAirport").val(validValue)
                }

                originAirport.value = validValue
            }
            else if (airportInput.id === "destinationAirport") {
                const validValue = GetIATAFromValue(airportInput.value)

                if (validValue != "") {
                    $("#destinationAirport").val(airportInput.value)
                }
                else {
                    $("#destinationAirport").val(validValue)
                }

                destinationAirport.value = validValue
            }
            else {
                console.log("ERROR - Invalid input id!")
            }
        }

        return {
            originAirport,
            destinationAirport,
            departureDate,
            returnDate,
            passangersNumber,
            currencyCode,
            resultFlightsInfo,
            submitInputs,
            getValue
        }

    },
    components: {
        AirportInputVue,
        FlightTableVue
    }
}
</script>

<style>
.input_form {
    display: flex;
    flex-direction: column;
    padding: 3rem;
    background: #2c3e50;
    color: #fff;
    -moz-border-radius: 25px;
    -webkit-border-radius: 25px;
    border: 3px solid black;
    border-radius: 25px;
    width: fit-content;
    margin: 0 auto;
}

.form_item {
    display: flex;
    flex-direction: column;
    margin: 1rem 0;
    width: 100%;
    position: relative;
}

.form_group {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    column-gap: 2rem;
}

.form_item input {
    width: 100%;
}

.form_item_label {
    border: 0;
    display: block;
}

.input_form input,
.input_form select {
    height: 25px;
    line-height: 25px;
    background: #fff;
    color: #000;
    padding: 0 6px;
    -moz-box-sizing: border-box;
    -webkit-box-sizing: border-box;
    box-sizing: border-box;
}

.form_item.custom_checkbox {
    display: flex;
    flex-direction: row;
}

.form_item.custom_checkbox input {
    padding: 0;
    width: 25px;
    height: fit-content;
    align-self: center;
}

.form_item.custom_checkbox label {
    align-self: center;
}

.form_item .range_display {
    width: fit-content;
    background-color: white;
    color: black;
    margin: 0.2rem 0;
    padding: 0 0.5em;
}

.form_button {
    margin: 0 auto;
    padding: 5px;
}

.form_button input[type="button"] {
    background-color: #dadada;
    height: unset;
    padding: 5px 10px;
    width: 100px;
    border-radius: 20px;
}

.form_button input[type="button"]:hover {
    background-color: white;
    border-color: black;
    transform: scale(1.1);
    transition: transform 0.2s ease-in-out;
}

.input_form label.error {
    font-size: 12px;
    color: #ff0000;
    position: absolute;
    bottom: -20px;
}

.input_form input.error {
    border: 2px solid red;
}
</style>