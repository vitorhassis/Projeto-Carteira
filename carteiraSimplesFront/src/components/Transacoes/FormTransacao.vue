<script setup>
import { ref, computed, watch } from "vue";

const tipo = ref("");
const categoria = ref("");

const categorias = computed(() => {
  if (tipo.value === "receita") {
    return ["Salário", "Freelance", "Investimentos", "Outros"];
  }

  if (tipo.value === "despesa") {
    return ["Alimentação", "Transporte", "Lazer", "Saúde", "Outros"];
  }

  return [];
});

// limpa categoria quando muda o tipo
watch(tipo, () => {
  categoria.value = "";
});
</script>

<template>
  <section class="bg-white border border-gray-300 rounded-xl h-[610px] p-6">
    <h1 class="text-2xl">Nova Transação</h1>

    <div class="flex flex-col gap-8">
      <!--Tipo-->
      <div class="pt-8 flex flex-col gap-2">
        <h2>Tipo de Transação</h2>
        <div class="flex gap-6 justify-center">
          <button
            @click="tipo = 'receita'"
            class="border-2 w-1/2 p-3 duration-150 hover:border-green-500 transition:150"
            :class="
              tipo === 'receita'
                ? 'border-green-500 bg-green-100 rounded-xl text-green-600'
                : 'border-gray-400 rounded-xl bg-white'
            "
          >
            Receita
          </button>
          <button
            @click="tipo = 'despesa'"
            class="border-2 w-1/2 p-3 duration-150 hover:border-red-500 transition:150"
            :class="
              tipo === 'despesa'
                ? 'border-red-500 bg-red-100 rounded-xl text-red-600'
                : 'border-gray-400 rounded-xl bg-white'
            "
          >
            Despesa
          </button>
        </div>
      </div>

      <!--Descrição-->
      <div class="flex flex-col gap-2">
        <h2>Descrição</h2>
        <input
          type="text"
          placeholder="Digite a descrição da transação"
          class="w-full border-2 rounded-lg p-2"
        />
      </div>

      <!--Valor e Categoria-->
      <div class="flex gap-4">
        <div class="flex flex-col gap-2 w-1/2">
          <h2>Valor</h2>
          <input
            type="text"
            placeholder="R$ 0,00"
            class="w-full border-2 rounded-lg p-2"
          />
        </div>
        <div class="flex flex-col gap-2 w-1/3">
          <h2>Categoria</h2>
          <select
            v-model="categoria"
            :disabled="!tipo"
            class="w-full border-2 rounded-lg p-2 disabled:bg-gray-100"
          >
            <option disabled value="">
              {{
                tipo ? "Selecione uma categoria" : "Selecione o tipo primeiro"
              }}
            </option>

            <option v-for="cat in categorias" :key="cat" :value="cat">
              {{ cat }}
            </option>
          </select>
        </div>
      </div>

      <!--Data-->
      <div class="flex flex-col gap-2">
        <h2>Data</h2>
        <input type="date" class="w-full border-2 rounded-lg p-2">
      </div>

      <!--Botão-->
      <button class="w-full bg-green-700 text-white p-3 rounded-lg">Adicionar Transação</button>
    </div>
  </section>
</template>
