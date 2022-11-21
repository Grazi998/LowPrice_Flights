<!--eslint-disable-->
<template>
    <input class="airport_autocomplete_input" @change="passSelectedValue($event)" />
</template>

<script>
    import airportsInfo from '../assets/airports.json';

    var airportsInformation = [];

    for (let index in airportsInfo) {
        airportsInformation.push(airportsInfo[index].name + " - " + airportsInfo[index].IATA);

    }

    export default {
        mounted() {
            this.$nextTick(() => {
                $(".airport_autocomplete_input").autocomplete({
                    source: airportsInformation,
                    minLength: 2
                });
            });
        },
        methods: {
            passSelectedValue(ev) {
                this.$emit("getSelectedValue", ev.target)
            }
        }
    };
</script>

<style>
.ui-menu.ui-widget.ui-widget-content.ui-autocomplete.ui-front{
    font-size: 14px;
}
</style>