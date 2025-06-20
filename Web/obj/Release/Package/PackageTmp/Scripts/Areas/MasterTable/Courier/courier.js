export class Courier {
    constructor() {
        this.init();
    }

    init() {
        this.IdCourier = 0;
        this.Name = '';
        this.Active = 0;
        this.destinations = [];
        this.file = '';
        this.messageValidationDestination = '';
        this.messageValidation = '';
    }

    addDestination(destination) {
        this.destinations.push(destination);
    }

    clearDestination() {
        this.destinations = [];
    }

    validateDestination() {
        let message = '';
        if (this.IdCourier == 0) {
            message += '- Debe agregar un courier' + "<br>";
        }
        if (this.destinations.length == 0) {
            message += '- Debe agregar un destino' + "<br>";
        }
        this.destinations.forEach(d => {
            if (d.Ubigeo.Department === '' || d.Ubigeo.Department === 'Seleccione') {
                message += "- El Departamento es un dato obligatorio" + "<br>";
            }
            if (d.Ubigeo.Province === '' || d.Ubigeo.Province === 'Seleccione') {
                message += "- La Provincia es un dato obligatorio" + "<br>";
            }
            if (d.Ubigeo.District === '' || d.Ubigeo.District === 'Seleccione') {
                message += "- El Distrito es un dato obligatorio" + "<br>";
            }
            if (d.DeliveryTime === '') {
                message += "- El Tiempo de entrega es un dato obligatorio";
            }
        });
        this.messageValidationDestination = message;
    }

    isValidDestination() {
        return this.messageValidationDestination === '';
    }

    validate() {
        if (this.Name === '') {
            this.messageValidation = 'El courier es un dato obligatorio';
        }
    }

    isValidCourier() {
        return this.messageValidation === '';
    }
}

export class Destination{
    constructor(){
        this.IdDepartment = 0;
        this.IdProvince = 0;
        this.IdDistrict = 0;
        this.DeliveryTime = '';
        this.CodeUbigeo = '';
        this.Active = 1;
    }
}
