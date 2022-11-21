<template>
  <div class="table_wrapper container">
    <table v-show="flightData.length !== 0">
      <thead>
        <tr>
          <td>
            Origin Airport
          </td>
          <td>
            Destination Airport
          </td>
          <td>
            Departure Time
          </td>
          <td>
            Arrival Time
          </td>
          <td>
            Changings
          </td>
          <!-- <td>
            Passangers Num.
          </td> -->
          <td>
            Price
          </td>
          <td>
            Currency
          </td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="flight in flightData" :key="flight.id">
          <td>
            <ol>
              <li v-for="iti in flight.itineraries" :key="iti">
                {{ iti.segments[0].departure.iataCode }}
              </li>
            </ol>
          </td>
          <td>
            <ol>
              <li v-for="iti in flight.itineraries" :key="iti">
                {{ iti.segments[iti.segments.length - 1].arrival.iataCode }}
              </li>
            </ol>
          </td>
          <td>
            <ol>
              <li v-for="iti in flight.itineraries" :key="iti">
                <ul>
                  <li v-for="dateTime in iti.segments[0].departure.at.split('T')" :key="dateTime">
                    {{dateTime}}
                  </li>
                </ul>
              </li>
            </ol>
          </td>
          <td>
            <ol>
              <li v-for="iti in flight.itineraries" :key="iti">
                <ul>
                  <li v-for="dateTime in iti.segments[iti.segments.length - 1].departure.at.split('T')" :key="dateTime">
                    {{dateTime}}
                  </li>
                </ul>
              </li>
            </ol>
          </td>
          <td>
            <ol>
              <li v-for="iti in flight.itineraries" :key="iti">
                <span v-if="iti.segments.length == 1">
                  Direct
                </span>
                <span v-else>
                  {{iti.segments.length}} stops
                </span>
              </li>
            </ol>
          </td>
          <td>
            {{ flight.price.grandTotal }}
          </td>
          <td>
            {{ flight.price.currency }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { ref } from 'vue';

export default {
  setup() {
    const resultMessage = ref("")
    return {
      resultMessage
    }
  },
  props: {
    flightData: Object
  }
}
</script>

<style>
.table_wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.table_wrapper table {
  width: 100%;
  margin: 2rem;
  border: 3px solid black;
  border-spacing: 1px;
}

.table_wrapper thead {
  background-color: rgb(2, 19, 255);
  color: white;
}

.table_wrapper thead tr td {
  border-bottom: 3px solid black;
  padding: 1rem;
  text-align: center;
  font-weight: bold;
}

.table_wrapper tbody {
  background-color: aqua;
}

.table_wrapper tbody ol{
  list-style-type: custom_unordered;
}

.table_wrapper tbody ol li{
  width: fit-content;
}

.table_wrapper tbody tr td {
  border-bottom: 1px solid black;
  text-align: center;
  font-style: italic;
}
@counter-style custom_unordered{
  system: fixed;
  symbols: 'D: ' 'R: ';
  suffix: ' ';
}
</style>