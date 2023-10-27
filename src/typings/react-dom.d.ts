// Do remember that type augmentation is a temporary solution. Once the official types get updated, you should revert to using them and remove your custom typings.
import "react-dom";

declare module "react-dom" {
	function createRoot(
		container: Element,
		options?: { hydrate?: boolean }
	): Root;
}
