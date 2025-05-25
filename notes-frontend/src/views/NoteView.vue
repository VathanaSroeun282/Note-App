<template>
  <div class="p-4 max-w-4xl mx-auto">
    <h1 class="text-xl sm:text-2xl font-bold mb-4 text-center sm:text-left">📝 Note App</h1>

    <input v-model="search" placeholder="Search by title..." class="mb-4 p-2 border rounded w-full" />

    <div class="flex flex-col sm:flex-row sm:justify-between sm:items-center gap-2 mb-4">
      <button
        @click="createMode = true"
        class="flex items-center gap-2 w-full sm:w-auto bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded shadow"
      >
        <span><img src="../../public/plus.png" width="15" /></span>New Note
      </button>
      <button
        @click="sortAsc = !sortAsc"
        class="w-full sm:w-auto bg-gray-200 hover:bg-gray-300 text-black px-4 py-2 rounded shadow"
      >
        🔤 Sort {{ sortAsc ? 'Z-A' : 'A-Z' }}
      </button>
    </div>

    <div v-if="filteredNotes.length === 0 && !isLoading" class="text-center text-gray-500">
      No notes found.
    </div>
    <div v-if="isLoading" class="text-center text-gray-500">Loading notes...</div>

    <NoteList v-else :notes="filteredNotes" @edit="editNote" @delete="deleteNoteById" />

    <BaseModal v-if="createMode || selectedNote" @close="resetForm">
      <NoteForm
        :note="selectedNote"
        :loading="isSubmitting"
        @submit="handleSubmit"
        @cancel="resetForm"
        v-model="noteForm"
      />
    </BaseModal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { getNotes, createNote, updateNote, deleteNote, Note } from '@/services/noteService'
import NoteForm from '@/components/NoteForm.vue'
import NoteList from '@/components/NoteList.vue'
import BaseModal from '@/components/BaseModal.vue'
const notes = ref<Note[]>([])
const search = ref('')
const createMode = ref(false)
const selectedNote = ref<Note | null>(null)
const isLoading = ref(false)
const isSubmitting = ref(false)
const sortAsc = ref(true)

const noteForm = ref<Omit<Note, 'id' | 'createDate' | 'updateDate'>>({ title: '', content: '' })

const loadNotes = async () => {
  try {
    isLoading.value = true
    const res = await getNotes()
    notes.value = res.data
  } catch (err) {
    console.error('Failed to fetch notes:', err)
  } finally {
    isLoading.value = false
  }
}

onMounted(loadNotes)

const filteredNotes = computed(() => {
  const filtered = notes.value.filter((n) =>
    n.title.toLowerCase().includes(search.value.toLowerCase()),
  )
  return filtered.sort((a, b) => {
    return sortAsc.value ? a.title.localeCompare(b.title) : b.title.localeCompare(a.title)
  })
})

const resetForm = () => {
  noteForm.value = { title: '', content: '' }
  selectedNote.value = null
  createMode.value = false
}

const editNote = (note: Note) => {
  selectedNote.value = note
  noteForm.value = { title: note.title, content: note.content || '' }
  createMode.value = false
}

const handleSubmit = async () => {
  try {
    isSubmitting.value = true
    if (selectedNote.value) {
      await updateNote(selectedNote.value.id, noteForm.value)
    } else {
      await createNote(noteForm.value)
    }
    await loadNotes()
    resetForm()
  } catch (err) {
    console.error('Failed to save note:', err)
  } finally {
    isSubmitting.value = false
  }
}

const deleteNoteById = async (id: number) => {
  if (confirm('Delete this note?')) {
    try {
      await deleteNote(id)
      await loadNotes()
    } catch (err) {
      console.error('Failed to delete note:', err)
    }
  }
}
</script>
