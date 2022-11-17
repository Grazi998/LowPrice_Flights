<template>
    <input v-model="data.test" />
    <button @click="Click">Click ME</button>
</template>

<script>
    import { reactive } from 'vue';
    import axios from 'axios';

    export default {
        setup() {

            let data = reactive({
                test: "AAAA"
            })

            const Click = () => {
                axios("https://localhost:5002/api/Flights").then(res=>{{
                    console.log(res);
                }});
            }

            return {
                data,
                Click
            }
        }

    }
</script>