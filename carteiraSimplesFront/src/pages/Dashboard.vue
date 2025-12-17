<template>
        <div >
            <Navbar />

            <div class="max-w-[1400px] mx-auto px-8">
                <section class="mt-8 flex flex-col gap-10 mb-8">
                <GrupoCardsResumo />
                <GrupoGraficos />
                <FormTransacao />
                <HistoricoTransacoes />
            </section>
            </div>

            
        </div>

</template>

<script setup>
    import Navbar from '../components/Navbar/Navbar.vue';
    import GrupoCardsResumo from '../components/CardsResumo/GrupoCardsResumo.vue';
    import GraficoReceitasDespesas from '../components/Graficos/GraficoReceitasDespesas.vue';
    import GraficoDespesasCategoria from '../components/Graficos/GraficoDespesasCategoria.vue'; 
    import GrupoGraficos from '../components/Graficos/GrupoGraficos.vue';
    import FormTransacao from '../components/Transacoes/FormTransacao.vue';
    import HistoricoTransacoes from '../components/Transacoes/HistoricoTransacoes.vue';
</script>

<style>

</style>