import { Ubigeo } from '../../Shared/Ubigeo/ubigeo.js';

export class CourierView{
    constructor(courierModel, controller) {
        this.courierModel = courierModel;
        this.controller = controller;
        this.paginationCourier = new Pagination();
        this.paginationDestination = new Pagination(15);
        this.couriers = [];
        this.ubigeoSearch = new Ubigeo('.cbx_search_department', '.cbx_search_province', '.cbx_search_district', 'Todos');
        this.ubigeoSearch.loadDepartment();
        this.ubigeoDestination = null;
    }

    initEvent() {
        let _this = this;
        $('.cbx_search_department').on('change', function () {
            _this.ubigeoSearch.loadProvince();
        });
        $('.cbx_search_province').on('change', function () {
            _this.ubigeoSearch.loadDistrict();
        });

        $('.btn_new_courier').on('click', function () {
            $('.register').fadeIn(500);
            pagenum = 0;
            _this.controller.newCourier();
        });

        $('.btn_save_courier').on('click', function () {
            _this.setModel();
            _this.controller.save();
        });

        $('.btn_close_courier').on('click', function () {
            _this.closeForm();
        });

        $('.btn_search').on('click', function () {
            _this.controller.getListCouriers(_this.getFilter());
        });

        $('.btn_add_destination').on('click', function () {
            $('.register_destination').fadeIn(500);
            _this.ubigeoDestination = new Ubigeo('.cbx_destination_department', '.cbx_destination_province', '.cbx_destination_district', 'Seleccione');
            _this.ubigeoDestination.loadDepartment();
        });

        $('.cbx_destination_department').on('change', function () {
            _this.ubigeoDestination.loadProvince();
        });
        $('.cbx_destination_province').on('change', function () {
            _this.ubigeoDestination.loadDistrict();
        });
        $('.btn_close_courier_destination').on('click', function () {
            _this.closeFormDestination();
        });

        $('.btn_save_courier_destination').on('click', function () {
            _this.setModelCourierDestination();
            _this.controller.saveDestination();
        });
    }

    setModel() {
        this.courierModel.Name = $('.txt_courier_name').val();
        this.courierModel.Active = $('.cbx_active').val();
        this.courierModel.file = $("#FileExcelWithDestinatios").prop('files')[0];
        this.courierModel.destinations = null;
    }

    getFilter() {
        return {
            Name: $('.txt_search_courier').val(),
            IdDepartment: $('.cbx_search_department').val(),
            IdProvince: $('.cbx_search_province').val(),
            IdDistrict: $('.cbx_search_district').val(),
            P_NPAGENUM: pagenum,
            P_NPAGESIZE: this.paginationCourier.pagesize
        };
    }

    setModelCourierDestination() {
        if (this.courierModel.destinations.length === 0) {
            this.courierModel.addDestination({
                Ubigeo: {
                    Department: $(".cbx_destination_department option:selected").text(),
                    Province: $(".cbx_destination_province option:selected").text(),
                    District: $(".cbx_destination_district option:selected").text()
                },
                DeliveryTime: $(".txt_destination_delivery_time").val(),
                Active: $(".cbx_destination_active").val()
            });
        }
        else {
            this.courierModel.destinations.forEach(d => {
                d.Ubigeo = {
                    Department: $(".cbx_destination_department option:selected").text(),
                    Province: $(".cbx_destination_province option:selected").text(),
                    District: $(".cbx_destination_district option:selected").text()
                };
                d.DeliveryTime = $(".txt_destination_delivery_time").val();
                d.Active = $(".cbx_destination_active").val();
            });
        }
        
    }

    getDestinationFilter() {
        return {
            IdCourier: this.courierModel.IdCourier,
            PageNumber: pagenum,
            PageSize: this.paginationDestination.pagesize
        };
    }

    showMessageValidation() {
        alertify.dialog('alert').set({
            transition: 'zoom',
            message: this.courierModel.messageValidation,
            title: 'SISPOC'
        }).show();
    }

    showMessageValidationDestination() {
        alertify.dialog('alert').set({
            transition: 'zoom',
            message: this.courierModel.messageValidationDestination,
            title: 'SISPOC'
        }).show();
    }

    loadCourier(data) {
        $('.register').fadeIn(500);
        $('#txt_courier_name').val(data.Name);
        $('#cbx_active').val(data.Active);
        this.loadDestinationsGrid(data.Destinations);
    }

    loadCourierDestination(data) {
        $('.register_destination').fadeIn(500);
        $('.txt_destination_delivery_time').val(data.DeliveryTime);
        $('#cbx_destination_active').val(data.Active);
        this.ubigeoDestination = new Ubigeo('.cbx_destination_department', '.cbx_destination_province', '.cbx_destination_district', 'Seleccione', data.Ubigeo);
        this.ubigeoDestination.loadDepartment();
    }

    loadCouriersGrid(data) {
        let jsonHeader = {
            Column1: 'NID',
            Column2: 'Courier',
            Column3: 'Departamento',
            Column4: 'Provincia',
            Column5: 'Distrito',
            Column6: 'Tiempo de Entrega',
            Column7: 'Habiliatdo'
        };
        let propsCol = {
            "Columns": [
                {
                    "col": "1",
                    "name": "NID",
                    "props": [
                        {
                            "visible": false
                        }
                    ]
                },
                {
                    "col": "2",
                    "name": "Courier",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "3",
                    "name": "Departamento",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "4",
                    "name": "Provincia",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "5",
                    "name": "Distrito",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "6",
                    "name": "Tiempo de Entrega",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "7",
                    "name": "Habilitado",
                    "props": [
                        {
                            "visible": false
                        }
                    ]
                }
            ]
        };
        let initTable = {
            dataTable: data,
            tableId: '.data-couriers',
            props: propsCol,
            tableHeader: jsonHeader,
            colCheckAll: false,
            editButton: true,
            delButton: false,
            colSequence: false,
            pagesize: this.paginationCourier.pagesize,
            callerId: 'loadCouriers',
            instanceId: 'listCouriers'
        };
        listCouriers = new renderTable(initTable);
        let _this = this;
        $('.btnRowEdit').on('click', function () {
            pagenum = 0;
            _this.courierModel.IdCourier = $(this).attr('id').replace('btn_e_row_', '');
            _this.controller.editCourier();
        });
    }

    loadDestinationsGrid(data) {
        let jsonHeader = {
            Column1: 'NID',
            Column2: 'Cod. Ubieo',
            Column2: 'Departamento',
            Column4: 'Provincia',
            Column5: 'Distrito',
            Column6: 'Tiempo de Entrega',
            Column7: 'Habiliatdo'
        };
        let propsCol = {
            "Columns": [
                {
                    "col": "1",
                    "name": "NID",
                    "props": [
                        {
                            "visible": false
                        }
                    ]
                },
                {
                    "col": "2",
                    "name": "Cod. Ubigeo",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "3",
                    "name": "Departamento",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "4",
                    "name": "Provincia",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "5",
                    "name": "Distrito",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "6",
                    "name": "Tiempo de Entrega",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                },
                {
                    "col": "7",
                    "name": "Habilitado",
                    "props": [
                        {
                            "visible": true
                        }
                    ]
                }
            ]
        };
        let initTable = {
            dataTable: data,
            tableId: '.data-couriers_destinatios',
            props: propsCol,
            tableHeader: jsonHeader,
            colCheckAll: false,
            editButton: true,
            delButton: false,
            colSequence: false,
            pagesize: this.paginationDestination.pagesize,
            callerId: 'loadDestinationsByCourier',
            instanceId: 'listDestinations'
        };
        listDestinations = new renderTable(initTable);
        let _this = this;
        $('.btnRowEdit').on('click', function () {
            let codeUbigeo = $(this).attr('id').replace('btn_e_row_', '');
            _this.courierModel.clearDestination();
            _this.courierModel.addDestination({
                CodeUbigeo: codeUbigeo
            });
            _this.controller.editDestination();
        });
    }

    closeForm() {
        $('.register').fadeOut(500, function () {
        });
        this.cleanControls();
    }

    cleanControls() {
        $('#txt_courier_name').val('');
        this.loadDestinationsGrid([]);
        this.courierModel.init();
    }

    cleanDestinationControls() {
        $('.txt_destination_delivery_time').val('');
        $('.cbx_destination_department').val('00');
        $('.cbx_destination_province').val('00');
        $('.cbx_destination_district').val('00');
        this.courierModel.clearDestination();
    }

    returnSave(result) {
        alertify.dialog('alert').set({
            transition: 'zoom',
            message: result.MENSAJE,
            title: 'SISPOC'
        }).show();
        if (result.EXITO === 1) {
            this.closeForm();
            this.controller.getListCouriers(this.getFilter(), this);
        }
    }

    returnDestinationSave(result) {
        alertify.dialog('alert').set({
            transition: 'zoom',
            message: result.MENSAJE,
            title: 'SISPOC'
        }).show();
        if (result.EXITO === 1) {
            this.closeFormDestination();
            this.controller.getDestinationsByCourier(this.getDestinationFilter());
        }
    }

    closeFormDestination() {
        $('.register_destination').fadeOut(500);
        this.cleanDestinationControls();
    }
}

export class Pagination {
    constructor(pagesize = 10) {
        this.pagenumber = 0;
        this.pagesize = pagesize;
    }
}