// src/models/HandleError.ts

export interface ErrorState {
  title: string;
  message: string;
  icon: string;
}

export interface IHandleError {
  (error: any): void; // Cette fonction prend une erreur et ne renvoie rien
}
