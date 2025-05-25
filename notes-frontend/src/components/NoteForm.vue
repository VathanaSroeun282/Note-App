<template>
  <div class="mt-6 border-t pt-4">
    <h2 class="text-xl font-bold mb-2">{{ note ? 'Edit Note' : 'Create Note' }}</h2>

    <form @submit.prevent="$emit('submit')" class="space-y-3">
      <input
        v-model="modelValue.title"
        placeholder="Title"
        required
        class="w-full p-2 border rounded"
      />
      <textarea
        v-model="modelValue.content"
        placeholder="Content"
        rows="4"
        class="w-full p-2 border rounded"
      />
      <div class="flex flex-col sm:flex-row gap-2">
        <button
          type="submit"
          :disabled="loading"
          class="w-full sm:w-auto bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded shadow disabled:opacity-50"
        >
          💾 {{ loading ? 'Saving...' : 'Save' }}
        </button>
        <button
          type="button"
          @click="$emit('cancel')"
          :disabled="loading"
          class="w-full sm:w-auto bg-gray-300 hover:bg-gray-400 text-gray-800 px-4 py-2 rounded shadow"
        >
          ❌ Cancel
        </button>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  note: any
  loading: boolean
  modelValue: {
    title: string
    content: string
  }
}>()

defineEmits(['submit', 'cancel', 'update:modelValue'])
</script>