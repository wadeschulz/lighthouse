var options = {
    allow_empty: true,

    default_filter: '',

    optgroups: {
        core: {
            en: 'Core'
        }
    },

    plugins: {
        'bt-tooltip-errors': { delay: 100 },
        'sortable': null,
        'filter-description': { mode: 'bootbox' },
        'bt-selectpicker': null,
        'unique-filter': null,
        'bt-checkbox': { color: 'primary' },
        'invert': null
    },

    filters: [
        /*
        * Gene Name
        */
        {
            id: 'gene',
            label: 'Gene',
            type: 'string',
            input: 'select',
            multiple: false,
            operators: ['equal', 'not_equal', 'is_null', 'is_not_null'],
            plugin: 'selectize',
            plugin_config: {
                valueField: 'id',
                labelField: 'name',
                searchField: 'name',
                sortField: 'name',
                options: [
                    { id: "ASXL1", name: "ASXL1" },
                    { id: "CBL", name: "CBL" },
                    { id: "CEBPA", name: "CEBPA" },
                    { id: "CSF3R", name: "CSF3R" },
                    { id: "DNMT3A", name: "DNMT3A" },
                    { id: "ETV6", name: "ETV6" },
                    { id: "EZH2", name: "EZH2" },
                    { id: "FLT3", name: "FLT3" },
                    { id: "HRAS", name: "HRAS" },
                    { id: "IDH1", name: "IDH1" },
                    { id: "IDH2", name: "IDH2" },
                    { id: "JAK2", name: "JAK2" },
                    { id: "KIT", name: "KIT" },
                    { id: "KRAS", name: "KRAS" },
                    { id: "MLL", name: "MLL" },
                    { id: "MPL", name: "MPL" },
                    { id: "NPM1", name: "NPM1" },
                    { id: "NRAS", name: "NRAS" },
                    { id: "PHF6", name: "PHF6" },
                    { id: "RUNX1", name: "RUNX1" },
                    { id: "SF3B1", name: "SF3B1" },
                    { id: "SRSF2", name: "SRSF2" },
                    { id: "TET2", name: "TET2" },
                    { id: "TP53", name: "TP53" },
                    { id: "WT1", name: "WT1" }
                ]
            },
            valueSetter: function (rule, value) {
                rule.$el.find('.rule-value-container select')[0].selectize.setValue(value);
            }
        },
        {
            id: 'af',
            label: 'Allelic Frequency',
            type: 'double',
            size: 10,
            validation: {
                min: 0,
                step: 0.5,
                max: 100
            }
        },
        {
            id: 'chr',
            label: 'Chromosome',
            operators: ['equal', 'not_equal', 'is_null', 'is_not_null'],
            type: 'string',
            size: 5
        },
        {
            id: 'loc',
            label: 'Genomic Location',
            type: 'integer',
            size: 10
        },
        {
            id: 'region',
            label: 'Genomic Region',
            type: 'string',
            input: 'select',
            multiple: false,
            operators: ['equal', 'not_equal', 'is_null', 'is_not_null'],
            plugin: 'selectize',
            plugin_config: {
                valueField: 'id',
                labelField: 'name',
                searchField: 'name',
                sortField: 'name',
                options: [
                    { id: "intronic", name: "Intronic" },
                    { id: "exonic", name: "Exonic" },
                    { id: "splicesite", name: "Splicesite" },
                    { id: "utr_5", name: "5' UTR" },
                    { id: "utr_3", name: "3' UTR" },
                    { id: "upstream", name: "Upstream" },
                    { id: "downstream", name: "Downstream" }
                ]
            },
            valueSetter: function (rule, value) {
                rule.$el.find('.rule-value-container select')[0].selectize.setValue(value);
            }
        },
        {
            id: 'vartype',
            label: 'Variant Type',
            type: 'string',
            input: 'select',
            multiple: false,
            operators: ['equal', 'not_equal', 'is_null', 'is_not_null'],
            plugin: 'selectize',
            plugin_config: {
                valueField: 'id',
                labelField: 'name',
                searchField: 'name',
                sortField: 'name',
                options: [
                    { id: "snv", name: "Single Nucleotide Variant (SNV/SNP)" },
                    { id: "indel", name: "Insertion/Deletion (INDEL)" },
                    { id: "mnv", name: "Multiple Nucleotide Variant (MNV)" }
                ]
            },
            valueSetter: function (rule, value) {
                rule.$el.find('.rule-value-container select')[0].selectize.setValue(value);
            }
        },
        {
            id: 'vareffect',
            label: 'Variant Effect',
            type: 'string',
            input: 'select',
            multiple: false,
            operators: ['equal', 'not_equal', 'is_null', 'is_not_null'],
            plugin: 'selectize',
            plugin_config: {
                valueField: 'id',
                labelField: 'name',
                searchField: 'name',
                sortField: 'name',
                options: [
                    { id: "synonymous", name: "Synonymous" },
                    { id: "missense", name: "Missense" },
                    { id: "frameshiftInsertion", name: "Frameshift Insertion" },
                    { id: "frameshiftDeletion", name: "Frameshift Deletion" }
                ]
            },
            valueSetter: function (rule, value) {
                rule.$el.find('.rule-value-container select')[0].selectize.setValue(value);
            }
        }
    ]
};

// init
$('#builder').queryBuilder(options);

$('#builder').on('afterCreateRuleInput.queryBuilder', function (e, rule) {
    if (rule.filter.plugin == 'selectize') {
        rule.$el.find('.rule-value-container').css('min-width', '200px')
            .find('.selectize-control').removeClass('form-control');
    }
});

// set rules from MongoDB
$('.set-mongo').on('click', function () {
    $('#builder').queryBuilder('setRulesFromMongo', {
        "$and": [
            {
                "name": {
                    "$regex": "^(?!Mistic)"
                }
            }, {
                "price": { "$gte": 0, "$lte": 100 }
            }, {
                "$or": [
                    {
                        "category": 2
                    }, {
                        "category": { "$in": [4, 5] }
                    }
                ]
            }
        ]
    });
});

// set rules from SQL
$('.set-sql').on('click', function () {
    $('#builder').queryBuilder('setRulesFromSQL', 'name NOT LIKE "Mistic%" AND price BETWEEN 100 AND 200 AND (category IN(1, 2) OR rate <= 2)');
});

// reset builder
$('.reset').on('click', function () {
    $('#builder').queryBuilder('reset');
    $('#result').addClass('hide').find('pre').empty();
});

// get rules
$('.parse-json').on('click', function () {
    $('#result').removeClass('hide')
        .find('pre').html(JSON.stringify(
            $('#builder').queryBuilder('getRules'),
            undefined, 2
        ));
});

$('.parse-sql').on('click', function () {
    var res = $('#builder').queryBuilder('getSQL', $(this).data('stmt'), false);
    $('#result').removeClass('hide')
        .find('pre').html(
            res.sql + (res.params ? '\n\n' + JSON.stringify(res.params, undefined, 2) : '')
        );
});

$('.parse-mongo').on('click', function () {
    $('#result').removeClass('hide')
        .find('pre').html(JSON.stringify(
            $('#builder').queryBuilder('getMongo'),
            undefined, 2
        ));
});