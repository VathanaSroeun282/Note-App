import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5142/api',
});

export interface Note {
    id: number;
    title: string;
    content: string;
    createDate: string;
    updateDate: string;
}

// CRUD Operations
export const getNotes = () => api.get<Note[]>('/Note');
export const getNoteById = (id: number) => api.get<Note>(`/Note/${id}`);
export const createNote = (note: Omit<Note, 'id' | 'createDate' | 'updateDate'>) => api.post<Note>('/Note', note);
export const updateNote = (id: number, note: Partial<Note>) => api.put<Note>(`/Note/${id}`, note);
export const deleteNote = (id: number) => api.delete<void>(`/Note/${id}`);