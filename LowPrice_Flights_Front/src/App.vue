<template>
    <Home />
    <LoaderDotsVue />
</template>

<script>
    import Home from './components/Home.vue';
    import LoaderDotsVue from './components/LoaderDots.vue'


    export default {
        components: {
            Home,
            LoaderDotsVue
        }
    }
</script>

<style>
    .container {
        max-width: calc(1440px - 2rem);
        margin: 0 auto;
    }
</style>
