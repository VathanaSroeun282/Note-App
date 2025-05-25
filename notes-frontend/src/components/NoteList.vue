<template>
  <ul class="flex flex-wrap justify-between">
    <li
      v-for="note in notes"
      :key="note.id"
      class="w-[49%] border rounded p-4 mb-3 shadow-sm bg-blue-50"
    >
      <div class="flex flex-col sm:flex-row sm:justify-between sm:items-center gap-2">
        <div>
          <h2 class="font-semibold text-lg">{{ note.title }}</h2>
          <p class="text-sm">{{ note.content }}</p>
          <small class="text-gray-500">Created: {{ formatDate(note.createDate) }}</small>
        </div>
        <div class="flex gap-2">
          <button @click="$emit('edit', note)" class="text-yellow-500 hover:text-yellow-600">
            ✏️
          </button>
          <button @click="$emit('delete', note.id)" class="text-red-500 hover:text-red-600">
            🗑️
          </button>
        </div>
      </div>
    </li>
  </ul>
</template>

<script setup lang="ts">
import { Note } from '@/services/noteService'

defineProps<{ notes: Note[] }>()
defineEmits(['edit', 'delete'])

const formatDate = (date: string) =>
  new Date(date).toLocaleString(undefined, {
    dateStyle: 'medium',
  })
</script>
