import { useUserStore } from '../model/userStore';

const API_URL = 'http://localhost:5000/api'; // Replace with your actual API URL

export const fetchUsers = async () => {
  const response = await fetch(`${API_URL}/users`);
  if (!response.ok) {
    throw new Error('Failed to fetch users');
  }
  const users = await response.json();
  // You might want to update the store here if needed
  return users;
};

export const createUser = async (userData: { name: string }) => {
  const response = await fetch(`${API_URL}/users`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(userData),
  });
  if (!response.ok) {
    throw new Error('Failed to create user');
  }
  const newUser = await response.json();
  useUserStore.getState().setUser(newUser); // Example of updating the store
  return newUser;
};
