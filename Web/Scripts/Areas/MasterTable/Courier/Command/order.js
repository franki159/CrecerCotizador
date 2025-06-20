export class Order {
    constructor(courierView) {
        this.courierView = courierView;
    }

    loadCouriersGrid(data) {
        this.courierView.loadCouriersGrid(data);
    }

    loadDestinationsGrid(data) {
        this.courierView.loadDestinationsGrid(data);
    }

    returnSave(data) {
        this.courierView.returnSave(data);
    }

    returnDestinationSave(data) {
        this.courierView.returnDestinationSave(data);
    }

    loadCourier(data) {
        this.courierView.loadCourier(data);
    }

    loadCourierDestination(data) {
        this.courierView.loadCourierDestination(data);
    }

    showMessageValidationDestination() {
        this.courierView.showMessageValidationDestination();
    }

    showMessageValidation() {
        this.courierView.showMessageValidation();
    }
}
