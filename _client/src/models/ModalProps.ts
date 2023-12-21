// src/models/ModalProps.ts

export interface ModalProps {
	show: boolean;
	title: string;
	message: string;
	onClose: () => void;
	icon: React.ReactNode; // Le chemin ou la classe de l'icône.
}
