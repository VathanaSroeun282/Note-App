<template>
  <div class="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50">
    <div class="bg-white rounded-lg shadow-lg w-full max-w-lg p-6 relative">
      <button
        class="absolute top-2 right-2 text-gray-500 hover:text-red-500"
        @click="$emit('close')"
      >
        ✖️
      </button>
      <slot />
    </div>
  </div>
</template>

<script setup lang="ts">
defineEmits(['close']);
</script>
