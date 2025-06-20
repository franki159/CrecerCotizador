import { CourierService } from './service.js';

export class CourierController {
    constructor(courierModel) {
        this.courierModel = courierModel;
        this.courierService = new CourierService();
        this.order = null;
    }

    setOrder(order) {
        this.order = order;
        this.courierService.setOrder(order);
    }

    save() {
        this.courierModel.validate();
        if (this.courierModel.isValidCourier()) {
            this.courierService.save(this.courierModel);
        }
        else {
            this.order.showMessageValidation();
            this.courierModel.messageValidation = '';
        }
    }

    saveDestination() {
        this.courierModel.validateDestination();
        if (this.courierModel.isValidDestination()) {
            this.courierService.saveDestination(this.courierModel);
        }
        else {
            this.order.showMessageValidationDestination();
        }
    }

    getListCouriers(filter) {
        return this.courierService.getListCouriers(filter);
    }

    newCourier() {
        this.courierModel.init();
    }

    editCourier() {
        this.courierService.getCourier(this.courierModel.IdCourier);
    }

    getDestinationsByCourier(filter) {
        this.courierService.getDestinationsByCourier(filter);
    }

    editDestination() {
        let destination = {
            IdCourier: this.courierModel.IdCourier,
            CodeUbigeo: this.courierModel.destinations[0].CodeUbigeo
        };
        this.courierService.getDestination(destination);
    }
}