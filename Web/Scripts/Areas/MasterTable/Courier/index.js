import { CourierController } from './controller.js';
import { Courier } from './courier.js'
import { CourierView } from './courierView.js'
import { Order } from './Command/order.js'
let listCouriers;
let listDestinations;
let manager;

function main() {
    manager = new Manager();
    manager.initEvent();
    manager.assignElementsToWndowsObject();
}

class Manager {
    constructor() {
        this.courierModel = new Courier();
        this.courierController = new CourierController(this.courierModel);
        this.courierView = new CourierView(this.courierModel, this.courierController);
        this.courierController.setOrder(new Order(this.courierView));
    }

    initEvent() {
        this.courierView.initEvent();
    }

    assignElementsToWndowsObject() {
        window.listCouriers = listCouriers;
        window.listDestinations = listDestinations;
        window.loadCouriers = loadCouriers;
        window.loadDestinationsByCourier = loadDestinationsByCourier;
    }
}

main();

function loadCouriers() {
    manager.courierController.getListCouriers(manager.courierView.getFilter());
}

function loadDestinationsByCourier() {
    manager.courierController.getDestinationsByCourier(manager.courierView.getDestinationFilter());
}